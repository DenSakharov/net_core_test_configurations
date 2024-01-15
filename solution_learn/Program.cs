using httpClient;
using solution_learn.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<IStockSetvice, StockService>();

///��� ��������� �������� ��������������� � ����� ���
//builder.Services.AddHttpClient<IStockHttpClient,StockHttpClient>();

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
})

app.Run();