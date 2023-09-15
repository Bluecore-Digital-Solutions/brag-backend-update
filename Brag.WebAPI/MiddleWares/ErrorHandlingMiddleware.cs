
using Brag.Domain.Common;
using Brag.Domain.Common.Enums;
using Brag.Domain.Exceptions;
using Serilog;
using System.Text.Json;

namespace Brag.WebAPI.MiddleWares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task InvokeAsync(HttpContext context)
        {
            var response = context.Response;
            var request = context.Request;
            response.ContentType = "application/json";
            var responseResult = new ApiResponse<string>();
            try
            {
                await _next(context);
            }
            catch (NotFoundException e)
            {
                response.StatusCode = StatusCodes.Status404NotFound;
                responseResult.Message = e.Message;
                responseResult.Status = false;
                responseResult.StatusCode = StatusEnum.NoRecordFound;

                ApiLogs.Info("NotFoundException : " + e.Message);
            }

            catch (ValidationException e)
            {


                response.StatusCode = StatusCodes.Status400BadRequest;
                responseResult.Message = e.Message;
                responseResult.Errors = e.Errors;
                responseResult.StatusCode = StatusEnum.Validation;

                ApiLogs.Warning("ValidationException: " + e.Message);
                //Warning(e, e.Message);
            }
            catch (UnauthorisedException e)
            {
                Log.Error(e, e.Message);
                response.StatusCode = StatusCodes.Status401Unauthorized;
                responseResult.StatusCode = StatusEnum.Unauthorised;
                responseResult.Message = e.Message;
                responseResult.Status = false;
            }
            catch (ApiGenericException e)
            {

                response.StatusCode = StatusCodes.Status200OK;
                responseResult.Message = e.Message; //="Wait!,We have this message for you :- " +
                responseResult.StatusCode = StatusEnum.Message;
                responseResult.Status = false;
                ApiLogs.Info("We have this message for you  : " + e.Message);
            }
            catch (InvalidRequestException e)
            {
                Log.Error(e, e.Message);
                response.StatusCode = StatusCodes.Status400BadRequest;

                responseResult.Message = e.Message;
                responseResult.StatusCode = StatusEnum.SystemError;
                responseResult.Status = false;
                ApiLogs.Error("Bad Request: ", e);

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);

                response.StatusCode = StatusCodes.Status500InternalServerError;
                responseResult.Status = false;
                responseResult.Message = "An error occurred.  " + e.Message.ToString();// 
                responseResult.StatusCode = StatusEnum.SystemError;
               
                ApiLogs.Error("Server Error Exception: ", e);
            }
            finally
            {
                if (!responseResult.IsEmpty() && !response.HasStarted)
                {
                    var result = JsonSerializer.Serialize(responseResult, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                    //Encript result here for error response
                  //  string jsonString = JsonSerializer.Serialize(m, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                    await response.WriteAsync(result);
                }
            }
        }
    }
    
    
}
