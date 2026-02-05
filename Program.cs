using InfotecsTestWebApi.Models;
using InfotecsTestWebApi.Services;
using InfotecsTestWebApi.Services.Csv;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<CsvReading>();
builder.Services.AddScoped<CsvValidator>();
builder.Services.AddScoped<CsvAggregator>();
builder.Services.AddScoped<FileProcessingService>();
builder.Services.AddScoped<FilterService>();
builder.Services.AddScoped<ValuesService>();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


