using gRPC;
using Grpc.Net.Client;

using var channel = GrpcChannel.ForAddress("https://localhost:7119");
var client = new StockApiGrpc.StockApiGrpcClient(channel);

var response=await client.GetAllStockItemsAsync(new GetAllStockItemsRequest(),cancellationToken: CancellationToken.None);
foreach (var item in response.Stocks)
{
    Console.WriteLine($"ItemId {item.ItemId} ; ItemName {item.ItemName} ; Quantity {item.Quantity}");
}
