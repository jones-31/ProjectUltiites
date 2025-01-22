using Microsoft.AspNetCore.Http;
using ProjectUtilities.Logger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectUtilities.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ICustomLogger _logger;

        public CustomExceptionMiddleware(RequestDelegate requestDelegate, ICustomLogger logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                LogExceptionDetails(ex);
                await RedirectToErrorPage(context, ex);
            }
        }

        private void LogExceptionDetails(Exception exception)
        {
            if (exception != null)
            {
                var stacktrace = new StackTrace(exception, true);
                var frame = stacktrace.GetFrame(0);
                var method = frame.GetMethod();
                var classname = method?.DeclaringType?.FullName ?? "Unknown_Class";
                var methodname = method?.Name ?? "Unknown_Method";
                var linenumber = frame.GetFileLineNumber();

                _logger.LogError(exception, classname, methodname, linenumber.ToString());
            }
        }

        private Task RedirectToErrorPage(HttpContext context, Exception exception)
        {

            var errorPageURL = $"/Home/Error404?message={Uri.EscapeDataString(exception.Message)}";
            context.Response.Redirect(errorPageURL);
            return Task.CompletedTask;
        }
    }
}
