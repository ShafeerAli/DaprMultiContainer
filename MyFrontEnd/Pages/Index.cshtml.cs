using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyFrontEnd.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DaprClient _daprClient;

        public IndexModel(DaprClient daprClient, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        public async Task OnGet()
        {
            var forecasts = await _daprClient.InvokeMethodGrpcAsync<Google.Protobuf.IMessage>("mybackend", "weatherforecast");
            ViewData["WeatherForecastData"] = forecasts;
        }
    }
}