using Final.Authorization;
using Final.Data;
using Final.Helpers;
using Final.MailServices;
using Final.Repositories.Search;
using Final.Repositories.Order;
using Final.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json.Serialization;
using HomeZilla_Backend.Repositories.Customers;
using HomeZilla_Backend.Services.BlobServices;
using HomeZilla_Backend.Repositories.Providers;
using HomeZilla_Backend.Repositories.Auth;
using HomeZilla_Backend.Repositories.Analytics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));

// Service Dependecy Injection
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddScoped<IBlobService, BlobService>();
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped<IAuthRepo, AuthRepo>();
builder.Services.AddScoped<ISearchRepo, SearchRepo>();
builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddScoped<IProviderRepo, ProviderRepo>();
builder.Services.AddScoped<IAnalyticsRepo, AnalyticsRepo>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(

         options =>
         {
             options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
             {
                 Name = "Authorization",
                 Type = SecuritySchemeType.Http,
                 Scheme = "Bearer",
                 BearerFormat = "JWT",
                 In = ParameterLocation.Header,
                 Description = "JWT Authorization header using the Bearer scheme."
             });
             options.OperationFilter<SecurityRequirementsOperationFilter>();
             options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                    Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                    }
                },
                new string[] {}
        }
    });
             options.SwaggerDoc("v1", new OpenApiInfo
             {
                 Version = "v1",
                 Title = "HomeZilla API",
                 Description = "Api for Home Service Portal",
                 // TermsOfService = new Uri(""),
                 Contact = new OpenApiContact
                 {
                     Name = "HomeZilla360 Team",
                     Email = "vishale2020@gmail.com",
                     Url = new Uri("https://github.com/Dark-Rex01"),
                 },
                 License = new OpenApiLicense
                 {
                     Name = "License Information",
                     // Url = new Uri("")
                 }
             });

         }
);


// configure strongly typed settings object
builder.Services.Configure<Final.Helpers.AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Injecting DbContext and Connection String
builder.Services.AddDbContext<HomezillaContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


// JSON Serializer
builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.{
    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();

// global error handler
app.UseMiddleware<ErrorHandler>();

// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .WithExposedHeaders("Authorization")
    .WithOrigins("http://localhost:4200", "https://localhost:4200"));

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
