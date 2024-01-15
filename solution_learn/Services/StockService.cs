using solution_learn.Controllers;

namespace solution_learn.Services;

class StockService : IStockSetvice
{
    private readonly List<StockItem> StockItems = new List<StockItem>
    {
        new StockItem(1,"Futbolka",10),
        new StockItem(2,"Tolstovka",20),
        new StockItem(3,"Kepka",30),
    };
    public Task<List<StockItem>> GetAll(CancellationToken toke) => Task.FromResult(StockItems);
    public Task<StockItem> GetById(long itemId, CancellationToken token)
    {
        var stockItem = StockItems.FirstOrDefault(x => x.ItemId == itemId);
        return Task.FromResult(stockItem);
    }
    public Task<StockItem> Add(StockItemCreationModel stockItem, CancellationToken token)
    {
        var itemId = StockItems.Max(x => x.ItemId) + 1;
        var newStovkItem = new StockItem(itemId, stockItem.ItemName, stockItem.Quantity);
        StockItems.Add(newStovkItem);
        return Task.FromResult(newStovkItem);
    }
}