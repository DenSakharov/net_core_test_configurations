using httpClient;
using solution_learn.Infrostructure.Middlewares;
using solution_learn.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<IStockSetvice, StockService>();

builder.Services.AddGrpc();

///��� ��������� �������� ��������������� � ����� ���
//builder.Services.AddHttpClient<IStockHttpClient,StockHttpClient>();

/// ���������� ������������ � ��������� ����� , ����������������� middleware
builder.Host.AddInfrostructre()
    // ������ ���������� ������������ � ���������� �������� ����������
    .AddHttp();

var app = builder.Build();

///��������� � ������ ������
app.Map("/version", builder => builder.UseMiddleware<VersionMiddleware>());

/// ����������� �������
app.UseMiddleware<RequestLoggingMiddleWare>();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<StockGrpcService>();
    endpoints.MapControllers();
});

app.Run();
