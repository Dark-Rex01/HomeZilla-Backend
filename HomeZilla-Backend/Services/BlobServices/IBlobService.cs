namespace HomeZilla_Backend.Services.BlobServices
{
    public interface IBlobService
    {
        Task<string> Upload(IFormFile files);
        Task Delete(string? FileName);
    }
}
