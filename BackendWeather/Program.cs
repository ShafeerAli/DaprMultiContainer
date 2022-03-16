using Dapr.Client;
using Dapr.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var client = new DaprClientBuilder()
                .Build();
// Add services to the container.
builder.Services.AddControllers().AddDapr();
builder.Services.AddDaprClient();
builder.Services.AddSingleton<DaprClient>(client);

builder.WebHost.ConfigureAppConfiguration(

    (configBuilder) =>  configBuilder.AddDaprSecretStore("backend2secrets",client)
);
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCloudEvents();
app.MapControllers();
app.MapSubscribeHandler();  

app.Run();
