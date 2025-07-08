using AgendaApp.Core.Domain.Exceptions;
using AgendaApp.Core.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace AgendaApp.Core.Domain.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate? _requestDelegate;

        public ExceptionMiddleware(RequestDelegate? requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)

        {
            try
            {
                await _requestDelegate(context);
            }
            catch (AgendaAppException e)
            {
                await HandleExceptionAsync(context, e);
            }
            catch (ApplicationException e)
            {
                await HandleExceptionAsync(context, e);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        public async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string mensagem = "Operação não realizada pois ocorreu um erro não tratado";

            switch (exception)
            {
                case AgendaAppException:
                    context.Response.StatusCode = ((AgendaAppException)exception).StatusCode;
                    mensagem = ((AgendaAppException)exception).Mensagem;
                    break;

                case ApplicationException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case Exception:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";

            var errorResultModel = new ErrorResultModel
            {
                StatusCode = context.Response.StatusCode,
                Message = mensagem,
            };

            await context.Response.WriteAsync(errorResultModel.ToString());
        }
    }
}
