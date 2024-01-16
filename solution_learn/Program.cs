using httpClient;
using solution_learn.Infrostructure.Middlewares;
using solution_learn.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<IStockSetvice, StockService>();

builder.Services.AddGrpc();

///как позволять клиентам интегрироваться с нашей апи
//builder.Services.AddHttpClient<IStockHttpClient,StockHttpClient>();

/// добавление конфигурации в отдельный класс , переиспользование middleware
builder.Host.AddInfrostructre()
    // сервис добавления контроллеров с глобальным фильтром исключения
    .AddHttp();

var app = builder.Build();

///обращение к версии сброки
app.Map("/version", builder => builder.UseMiddleware<VersionMiddleware>());

/// логирование запроса
app.UseMiddleware<RequestLoggingMiddleWare>();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<StockGrpcService>();
    endpoints.MapControllers();
});

app.Run();
