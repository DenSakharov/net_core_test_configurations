using httpClient;
using Microsoft.OpenApi.Models;
using solution_learn.Configuration.Middlewares;
using solution_learn.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<IStockSetvice, StockService>();

///��� ��������� �������� ��������������� � ����� ���
//builder.Services.AddHttpClient<IStockHttpClient,StockHttpClient>();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    ///���� ������� ������������ ����
    options.CustomSchemaIds(x => x.FullName);

    ///��������� ���� � xml docs
    var xmlFileName = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
    var xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
    options.IncludeXmlComments(xmlFilePath);

    /// ��������� 
    options.OperationFilter<HeaderOperationFilter>();
});

var app = builder.Build();

///��������� � ������ ������
app.Map("/version", builder => builder.UseMiddleware<VersionMiddleware>());

/// ����������� �������
app.UseMiddleware<RequestLoggingMiddleWare>();

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
