using Dapr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyBackend2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private const string STATESTORE_NAME = "statestore";

        [HttpGet()]
        public Task<int> GetBalance()
        {
            return Task.FromResult<int>(5);
        }

        //[HttpPost("/{accountNumber}/{amount}")]
        //public async Task<int> Deposit([FromState(STATESTORE_NAME, "accountNumber")] StateEntry<int> balance, int amount)
        //{
        //    balance.Value += amount;
        //    await balance.SaveAsync();
        //    return balance.Value;
        //}

        //[HttpDelete("/{accountNumber}/{amount}")]
        //public async Task<int> Withdraw([FromState(STATESTORE_NAME, "accountNumber")] StateEntry<int> balance, int amount)
        //{
        //    balance.Value -= amount;
        //    await balance.SaveAsync();
        //    return balance.Value;
        //}
    }
}
