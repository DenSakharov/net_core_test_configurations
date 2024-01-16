using gRPC;
using Grpc.Core;
using solution_learn.Services;

public class StockGrpcService: StockApiGrpc.StockApiGrpcBase
{
    private readonly IStockSetvice _stockSetvice;
    public StockGrpcService(IStockSetvice stockSetvice)
    {
        _stockSetvice = stockSetvice;
    }
    public override async Task<GetAllStockItemsResponce> GetAllStockItems(
        GetAllStockItemsRequest request, 
        ServerCallContext context)
    {
        var stockItem = await _stockSetvice.GetAll(context.CancellationToken);
        return new GetAllStockItemsResponce
        {
            Stocks = {stockItem.Select(x=>new GetAllStockItemsResponceUnit
            {
                ItemId = x.ItemId,
                Quantity = x.Quantity,
                ItemName=x.ItemName
            })}
        };
        //return base.GetAllStockItems(request, context);
    }
}