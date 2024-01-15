using Microsoft.AspNetCore.Mvc;
using solution_learn.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace solution_learn.Controllers
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
    [ApiController]
    [Route("v1/api/stocks")]
    ///возвращение определенного типа
    [Produces("application/json")]
    public class StockController:ControllerBase
    {
        private readonly IStockSetvice _stockSetvice;
        public StockController(IStockSetvice stockSetvice)
        {
            _stockSetvice = stockSetvice;
        }
        //Task<ActionResult<List<StockItem>>>
        [HttpGet]
        [ProducesResponseType(typeof(List<StockItem>), StatusCodes.Status200OK )]
        public async Task<ActionResult> GetAll(CancellationToken token)
        {
            var stockItems = await _stockSetvice.GetAll(token);
            return Ok(stockItems);
        }
        [HttpGet(template:"{id}")]
        [ProducesResponseType(typeof(StockItem), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StockItem>> GetById(long id,CancellationToken token)
        {
            var stockItems = await _stockSetvice.GetById(id,token);
            if (stockItems is null)
            {
                return NotFound();
            }
            return Ok(stockItems);
        }/// <summary>
         /// Добавляет Stock Item
         /// </summary>
         /// <param name="model"></param>
         /// <param name="token"></param>
         /// <returns></returns>
        [HttpPost]
        //[SwaggerResponse()]
        public async Task<ActionResult<StockItem>> Add(StockItemPostModel model,CancellationToken token)
        {
            var createdStockItem=await _stockSetvice.Add(new StockItemCreationModel
            {
                ItemName = model.ItemName,
                Quantity = model.Quantity
            }, token);
            return Ok(createdStockItem);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<StockItem>> Update(long id, StockItemPutModel model,CancellationToken token)
        {
            var stockItems = await _stockSetvice.GetById(id, token);
            if (stockItems is null)
            {
                return NotFound();
            }
            stockItems.ItemName = model.ItemName;
            stockItems.Quantity = model.Quantity;
            return Ok(stockItems);
        }
    }
} 
