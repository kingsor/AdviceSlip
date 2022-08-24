using AdviceSlipService.Models;
using AdviceSlipService.Services;
using AdviceSlipService.Services.Interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<CacheOptions>(
    builder.Configuration.GetSection("CacheOptions"));

builder.Services.AddScoped<IAdviceSlipProviderService, AdviceSlipProviderService>();

builder.Services.AddMemoryCache();

var app = builder.Build();

app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
var isSwaggerEnabled = builder.Configuration.GetValue<bool>("AppSettings:EnableSwagger");
if (isSwaggerEnabled)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();


public partial class Program { }
