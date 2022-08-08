using System.Text;
using Agoda.LogDb.Repos;
using Agoda.Models;

namespace Agoda.Common;

public class RequestLog
{
    private readonly RequestDelegate _next;
    public RequestLog(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IMongoUOW mongoUOW)
    {
        var originalResponseStream = context.Response.Body;

        var start = DateTime.Now;
        context.Request.EnableBuffering();

        var reQBody = new StreamReader(context.Request.Body);
        var reQContent = await reQBody.ReadToEndAsync();

        context.Request.Body.Position = 0;

        var reQpath = context.Request.Path.ToString();

        if (!reQpath.Contains("LogMonitor"))
        {
            string respose = "";

            using (var ms = new MemoryStream())
            {
                context.Response.Body = ms;
                await _next(context);

                ms.Position = 0;
                var responseReader = new StreamReader(ms);

                respose = responseReader.ReadToEnd();

                ms.Position = 0;
                await ms.CopyToAsync(originalResponseStream);
                context.Response.Body = originalResponseStream;
            }

            var log = new Logs()
            {
                RequestBody = reQContent,
                RequestDateTime = start,
                RequestPath = reQpath,
                RequestType = context.Request.Method,
                ResponseStatus = context.Response.StatusCode.ToString(),
                ResponseBody = respose,
                ResponseDateTime = DateTime.Now
            };

            await mongoUOW.LogsRepository.Add(log);
        }
        else
        {
            await _next(context);
        }
    }
}