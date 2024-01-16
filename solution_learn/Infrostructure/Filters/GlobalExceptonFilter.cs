using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using solution_learn.Controllers.V1;
/// <summary>
/// Глобальный фильтр исключения
/// </summary>
class GlobalExceptonFilter:ExceptionFilterAttribute
{
    private readonly ILogger<GlobalExceptonFilter> _logger;
    public GlobalExceptonFilter(ILogger<GlobalExceptonFilter> logger)
    {
        _logger = logger;
    }
    public override void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case CustomException exception:
                {
                    break;
                }
            default:
                {
                    break;
                }
        }
        base.OnException(context);
        var resultObject = new 
        { 
            Exception= context.Exception.GetType().FullName,
            Message=context.Exception.Message,
        };
        var jsonResult = new JsonResult(resultObject)
        {
            StatusCode = StatusCodes.Status500InternalServerError,
        };
        //_logger.LogError();
        context.Result = jsonResult;
    }
}