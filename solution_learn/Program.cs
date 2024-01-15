using httpClient;
using Microsoft.OpenApi.Models;
using solution_learn.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<IStockSetvice, StockService>();

///��� ��������� �������� ��������������� � ����� ���
//builder.Services.AddHttpClient<IStockHttpClient,StockHttpClient>();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title="API",Version="v1"});
    ///���� ������� ������������ ����
    options.CustomSchemaIds(x => x.FullName);
});

var app = builder.Build();
///Swagger Midlleware
app.UseSwagger();
/// ���������� �������� https://localhost:7119/swagger/index.html
app.UseSwaggerUI();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();