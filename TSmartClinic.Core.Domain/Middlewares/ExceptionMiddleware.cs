using TSmartClinic.Core.Domain.Exceptions;
using TSmartClinic.Core.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace TSmartClinic.Core.Domain.Middlewares
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
            catch (TSmartClinicException e)
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
                case TSmartClinicException:
                    context.Response.StatusCode = ((TSmartClinicException)exception).StatusCode;
                    mensagem = ((TSmartClinicException)exception).Mensagem;
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
