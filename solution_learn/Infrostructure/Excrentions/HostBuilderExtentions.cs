using Microsoft.OpenApi.Models;
using System.Reflection;

public static class HostBuilderExtentions
{
    public static IHostBuilder AddInfrostructre(this IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            /// возможность работы с middleware
            services.AddSingleton<IStartupFilter, SwaggerStartupFilter>();

            services.AddSwaggerGen(options =>
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
        });
        return builder;
    }
    public static IHostBuilder AddHttp(this IHostBuilder builder)
    {
        builder.ConfigureServices(services => 
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptonFilter>();
            });
        });
        return builder;
    }
}
