using Microsoft.AspNetCore.Mvc.Filters;

namespace PracticeCA.Api.Filters;

public class AuthorizationFilter : IAsyncActionFilter
{
    public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        throw new NotImplementedException();
    }

}