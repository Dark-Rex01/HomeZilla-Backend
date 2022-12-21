using Final.Authorization;
using Final.Data;
using Final.Helpers;
using Final.MailServices;
using Final.Services;
using Lucene.Net.Support;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));

// Service Dependecy Injection
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped<IAuthRepo, AuthRepo>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// configure strongly typed settings object
builder.Services.Configure<Final.Helpers.AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Injecting DbContext and Connection String
builder.Services.AddDbContext<HomezillaContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("_connectionString")));


// JSON Serializer
builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// global error handler
app.UseMiddleware<ErrorHandler>();

app.UseAuthorization();

app.MapControllers();

app.Run();
