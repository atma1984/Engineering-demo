using EngineeringSystem.Backend.Api.Contracts.Errors;
using EngineeringSystem.Backend.Domain.Common;
using System.Net;
using System.Text.Json;

namespace EngineeringSystem.Backend.Api.Middleware
{
    public sealed class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)                                 
        {
            try                                                                            
            {
                await _next(context);                                                      
            }
            catch (DomainException ex)                                                     
            {
                await WriteErrorResponse(                                                  
                    context: context,                                                      
                    statusCode: HttpStatusCode.BadRequest,                                 
                    errorMessage: ex.Message,                                              
                    exception: ex);                                                        
            }
            catch (KeyNotFoundException ex)                                                
            {
                await WriteErrorResponse(                                                  
                    context: context,                                                      
                    statusCode: HttpStatusCode.NotFound,                                   
                    errorMessage: "Ресурс не найден.",                                     
                    exception: ex);                                                        
            }
            catch (Exception ex)                                                           
            {
                await WriteErrorResponse(                                                  
                    context: context,                                                      
                    statusCode: HttpStatusCode.InternalServerError,                        
                    errorMessage: "Внутренняя ошибка сервера.",                            
                    exception: ex);                                                        
            }
        }

        private async Task WriteErrorResponse(                                               
                 HttpContext context,                                                        
                 HttpStatusCode statusCode,                                                  
                 string errorMessage,                                                        
                 Exception exception)                                                        
        {
            var traceId = context.TraceIdentifier;                                           

            _logger.LogError(                                                                
                exception,                                                                   
                "Unhandled exception. TraceId={TraceId}. Message={Message}",                 
                traceId,                                                                     
                errorMessage);                                                               

            if (context.Response.HasStarted)                                                 
            {
                _logger.LogWarning(                                                          
                    "Response has already started, cannot write error response. TraceId={TraceId}",
                    traceId);
                return;                                                                       
            }

            context.Response.Clear();                                                         
            context.Response.StatusCode = (int)statusCode;                                    
            context.Response.ContentType = "application/json; charset=utf-8";                 

            var payload = new ApiErrorResponse                                                
            {
                TraceId = traceId,                                                            
                Error = errorMessage                                                          
            };

            var json = JsonSerializer.Serialize(payload);                                     
            await context.Response.WriteAsync(json);                                          
        }
    



    }
}
