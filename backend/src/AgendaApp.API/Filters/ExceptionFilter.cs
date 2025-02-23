using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using AgendaApp.Application.Exceptions;
using AgendaApp.Domain.Exceptions;

namespace AgendaApp.API.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var result = new ObjectResult(new
            {
                Message = "Ocorreu um erro ao processar sua requisição.",
                ExceptionMessage = context.Exception.Message,
                ValidationErrors = context.Exception is ValidationException validationEx
                    ? validationEx.Errors?.Select(e => new { e.PropertyName, e.ErrorMessage })
                    : null
            });

            switch (context.Exception)
            {
                case DomainException:
                case ValidationException:
                    result.StatusCode = StatusCodes.Status400BadRequest;
                    break;

                case NotFoundException:
                    result.StatusCode = StatusCodes.Status404NotFound;
                    break;

                default:
                    _logger.LogError(context.Exception, "Erro não tratado");
                    result.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            context.Result = result;
        }
    }
}