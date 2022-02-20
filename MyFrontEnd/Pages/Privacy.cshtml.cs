using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyFrontEnd.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        private readonly DaprClient _daprClient;

        public PrivacyModel(ILogger<PrivacyModel> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        public async Task OnGet()
        {
            var data = new OrderData { orderId = "123456", productId = "67890", amount = 2 }; 
            await _daprClient.PublishEventAsync("pubsub", "newOrder", data);
        }
    }
}