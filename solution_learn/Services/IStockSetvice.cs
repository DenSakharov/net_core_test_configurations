using solution_learn.Controllers;

namespace solution_learn.Services;

public interface IStockSetvice
{
    Task<List<StockItem>> GetAll(CancellationToken token);
    Task<StockItem> GetById(long itemId, CancellationToken token);
    //Task<StockItem> Add(StockItem item, CancellationToken token);
    Task<StockItem> Add(StockItemCreationModel item, CancellationToken token);
}