using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DevXpert.Modulo3.API.Configurations.Extensions;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class AllowSynchronousIOAttribute : ActionFilterAttribute
{
    public AllowSynchronousIOAttribute()
    {
    }

    public override void OnResultExecuting(ResultExecutingContext context)
    {
        var syncIOFeature = context.HttpContext.Features.Get<IHttpBodyControlFeature>();

        if (syncIOFeature is not null) syncIOFeature.AllowSynchronousIO = true;
    }
}