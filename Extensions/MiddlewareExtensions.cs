using Agoda.Common;

namespace Agoda.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder useRequestLog(this IApplicationBuilder applicationBuilder) => applicationBuilder.UseMiddleware<RequestLog>();
}