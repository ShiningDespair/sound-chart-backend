using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Backend.ML_Models;
using Backend.Controllers;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Load secrets only in Development
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

Console.WriteLine("Loaded connection string:");
Console.WriteLine(builder.Configuration.GetConnectionString("ChinookContext"));


// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.SuppressOutputFormatterBuffering = true;
});



builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Soundchart API",
        Description = "An API to visualize data about music consumption from Chinook dataset",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Soundchart Team",
            Email = "info@duckheadsdev.com",
            Url = new Uri("https://duckheadsdev.com")
        }
    });
    var filename = Assembly.GetExecutingAssembly().GetName().Name +".xml";
    var filepath = Path.Combine(AppContext.BaseDirectory, filename);
    options.IncludeXmlComments(filepath);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var handler = new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
};

var client = new HttpClient(handler);


builder.Services.AddDbContext<ChinookContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ChinookContext")));

var app = builder.Build();

//Swagger setup 
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Soundchart API");
        options.RoutePrefix = "api/docs";
    });



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRouting();

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapStaticAssets();

app.MapControllers();



app.Run();

/// <summary>
/// Added for testing purposes but stays not functional for v1 version.
/// </summary>
public partial class Program { }
