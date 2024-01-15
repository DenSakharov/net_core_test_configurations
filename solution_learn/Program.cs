using httpClient;
using Microsoft.OpenApi.Models;
using solution_learn.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
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
public class HeaderOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        //context.ApiDescription = "v1/api/stocks/{id}";
        operation.Parameters ??= new List<OpenApiParameter>();
        operation.Parameters.Add(new OpenApiParameter
        {
            In = ParameterLocation.Header,
            Name = "our-header",
            Required=false,
            Schema=new OpenApiSchema { Type="string"}
        });
    }
}