using domain.entities;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using application.exceptions;
namespace api.Exceptions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly IProblemDetailsService _problemDetailsService;
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IProblemDetailsService problemDetailsService)
        {
            _logger = logger;
            _problemDetailsService = problemDetailsService;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext context, 
            Exception exception, 
            CancellationToken cancellation
        )
        {
            var statusCode = exception switch
            {
                ExceptionBase expB => (int)expB.status,
                InvalidOperationException => StatusCodes.Status400BadRequest,
                ArgumentOutOfRangeException => StatusCodes.Status400BadRequest,
                ArgumentException => StatusCodes.Status400BadRequest,
                DbUpdateException => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError,

            };

            // lấy ra mã lỗi 
            context.Response.StatusCode = statusCode;
            // trả ra problem 
            return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = context,
                Exception = exception,
                ProblemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
                {
                    Title = "Sảy ra lỗi",
                    Detail = exception is ExceptionBase api ? api.ErrorMessage : exception.Message,
                    Status = statusCode,
                    Instance = context.Request.Path
                }
            });
        }
    }
}