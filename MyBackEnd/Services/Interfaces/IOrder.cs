using Refit;


namespace MyBackEnd.Services.Interfaces
{
    public interface IOrder
    {
        [Get("/Order")]
        public Task<string> GetOrder();
    }
}
