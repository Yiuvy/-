using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Makarevich_proj.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class FileLogger
    {
        private readonly RequestDelegate _next;

        public FileLogger(RequestDelegate next) //указатель на следующий компонент в цепочке
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            await _next(httpContext); //запрос не обрабатывается, а сразу передаётся следующему компоненту
            var code = httpContext.Response.StatusCode; //когда приходит ответ, находим статус код
            var temp = code / 100;
            if (temp != 2) // не начинается с 2? -> записываем инфу в лог
                Log.Logger.Information($"-- Request {httpContext.Request.Path} returns{code}");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class FileLoggerExtensions
    {
        public static IApplicationBuilder UseFileLogger(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FileLogger>(); //метод для подключения 
        }
    }
}
