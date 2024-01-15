using httpModels;
using System.Collections.Generic;
using System.Text.Json;

namespace httpClient
{
    public interface IStockHttpClient
    {
        Task<List<StockItemResponce>> V1GetAll(CancellationToken token);
    }
    public class StockHttpClient:IStockHttpClient
    {
        private readonly HttpClient _httpClient;
        public StockHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<StockItemResponce>> V1GetAll(CancellationToken token)
        {
            using var responce = await _httpClient.GetAsync("v1/api/stocks",token);
            var body = await responce.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<List<StockItemResponce>>(body); 
        }
    }
}
