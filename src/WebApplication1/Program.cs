
using System.Text.Json;
using System.Text.Json.Serialization;

//var builder = WebApplication.CreateBuilder(args);
var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();


var weatherApi = app.MapGroup("/weather");

weatherApi.MapGet("/", () => new WeatherForecast[] {
    new WeatherForecast(DateOnly.FromDateTime(DateTime.Now), 10, "caldo"),
    new WeatherForecast(DateOnly.FromDateTime(DateTime.Now), 10, "freddo")});
weatherApi.MapGet("/{id}", (int id) =>
    Results.Ok(new WeatherForecast(DateOnly.FromDateTime(DateTime.Now), 10, "caldo")));

app.Run();

public class WeatherForecast(DateOnly date, int TemperatureC, string? summary)
{
    public string Status => $"{date.ToString("d")}, {TemperatureC}, {summary}";
}

[JsonSourceGenerationOptions(
    JsonSerializerDefaults.Web,
    AllowTrailingCommas = true)]
[JsonSerializable(typeof(WeatherForecast[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}