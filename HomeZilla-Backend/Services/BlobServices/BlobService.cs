using Azure.Storage.Blobs;
using Final.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.ComponentModel;

namespace HomeZilla_Backend.Services.BlobServices
{
    public class BlobService : IBlobService
    {
        private readonly IConfiguration _configuration;

        public BlobService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> Upload(IFormFile files)
        {
            string systemFileName = GenerateFileName(files.FileName);
            string blobstorageconnection = _configuration.GetValue<string>("BlobConnectionString");

            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);

            CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference(_configuration.GetValue<string>("BlobContainerName"));

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(systemFileName);

            blockBlob.Properties.ContentType = "image/jpg";
            await using (var data = files.OpenReadStream())
            {
                await blockBlob.UploadFromStreamAsync(data);
            }
            var fileUrl = blockBlob.Uri.AbsoluteUri;
            return fileUrl;
        }

        // Delete the image
        public async Task Delete(string FileName)
        {
            FileName = Path.GetFileName(FileName);
            BlobContainerClient client = new BlobContainerClient(_configuration.GetValue<string>("BlobConnectionString"), _configuration.GetValue<string>("BlobContainerName"));

            BlobClient file = client.GetBlobClient(FileName);
            await file.DeleteIfExistsAsync();
        }


        // Generate File Name
        private string GenerateFileName(string fileName)
        {
                string strFileName = string.Empty;
                string[] strName = fileName.Split('.');
                strFileName = DateTime.Now.ToUniversalTime().ToString("yyyyMMdd\\THHmmssfff") + "." +
                   strName[strName.Length - 1];
                return strFileName;
        }
    }
}
