using httpClient;
using Microsoft.OpenApi.Models;
using solution_learn.Configuration.Middlewares;
using solution_learn.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<IStockSetvice, StockService>();

///как позволять клиентам интегрироваться с нашей апи
//builder.Services.AddHttpClient<IStockHttpClient,StockHttpClient>();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    ///сбор полного пространства имен
    options.CustomSchemaIds(x => x.FullName);

    ///добавляем путь к xml docs
    var xmlFileName = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
    var xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
    options.IncludeXmlComments(xmlFilePath);

    /// добавляем 
    options.OperationFilter<HeaderOperationFilter>();
});

var app = builder.Build();

///обращение к версии сброки
app.Map("/version", builder => builder.UseMiddleware<VersionMiddleware>());

/// логирование запроса
app.UseMiddleware<RequestLoggingMiddleWare>();

///Swagger Midlleware
app.UseSwagger();
/// добавление страницы https://localhost:7119/swagger/index.html
app.UseSwaggerUI();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
