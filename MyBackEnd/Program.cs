using Dapr.Client;
using MyBackEnd;
using MyBackEnd.Services.Interfaces;
using Polly;
using Polly.Extensions.Http;
using Refit;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddDapr();
// Add services to the container.
builder.Services.AddDaprClient();

var client = DaprClient.CreateInvokeHttpClient("mybackend2");
var refit = Refit.RestService.For<IOrder>(client);

builder.Services.AddTransient<IOrder>(_ => refit);

//builder.Services.AddRefitClient<IOrder>().ConfigureHttpClient(a => a.BaseAddress = client.BaseAddress)
//    .SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Set lifetime to five minutes
//        .AddPolicyHandler(GetRetryPolicy());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["appinsightsconnectionstring"]);


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