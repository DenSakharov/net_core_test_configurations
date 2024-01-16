using Microsoft.AspNetCore.Mvc;
using solution_learn.Services;

namespace solution_learn.Controllers.V2
{
    /// <summary>
    /// Версионирование API
    /// </summary>
    [ApiController]
    [Route("v2/api/stocks")]
    public class V2StockController : ControllerBase
    {
        private readonly IStockSetvice _stockSetvice;
        public V2StockController(IStockSetvice stockSetvice)
        {
            _stockSetvice = stockSetvice;
        }
        [HttpPost]
        public async Task<ActionResult<StockItem>> Add(StockItemPostModelV2 model, CancellationToken token)
        {
            var createdStockItem = await _stockSetvice.Add(new StockItemCreationModel
            {
                ItemName = model.ItemName,
                Quantity = model.Quantity.value
            }, token);
            return Ok(createdStockItem);
        }
    }
}
