using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiCatalogo.Filters;

public class ApiLogginFilter : IActionFilter
{
    private readonly ILogger<ApiLogginFilter> _logger;

    public ApiLogginFilter(ILogger<ApiLogginFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        LogInformationHeader("OnActionExecuting");
        _logger.LogInformation($"Model State: {context.ModelState.IsValid}");
        LogInformationHashs();
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        LogInformationHeader("OnActionExecuted");
        _logger.LogInformation($"Status Code: {context.HttpContext.Response.StatusCode}");
        LogInformationHashs();
    }

    private void LogInformationHeader(string nomeMetodo)
    {
        _logger.LogInformation($"### Executando -> {nomeMetodo}");
        LogInformationHashs();
        _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
    }

    private void LogInformationHashs()
    {
        _logger.LogInformation("######################################################");
    }
}
