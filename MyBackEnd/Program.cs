using Dapr.Client;
using MyBackEnd;
using MyBackEnd.Services.Interfaces;
using Polly;
using Polly.Extensions.Http;
using Refit;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDaprClient();

var client = DaprClient.CreateInvokeHttpClient("mybackend2");
var refit = Refit.RestService.For<IWeatherForecast>(client);

builder.Services.AddTransient<IWeatherForecast>(_ => refit);




//builder.Services.AddTransient<IWeatherForecast>(_ => Refit.RestService.For<IWeatherForecast>(client));

builder.Services.AddControllers().AddDapr();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    return HttpPolicyExtensions
         .HandleTransientHttpError()
         .WaitAndRetryAsync(1, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                                                                     retryAttempt)));
}