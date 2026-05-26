using Common.Application;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Common.AspNetCore
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        protected ApiResult CommandResult(OperationResult result)
        {
            return new ApiResult()
            {
                IsSuccess = result.Status == OperationResultStatus.Success,
                MetaData = new MetaData()
                {
                    Message = result.Message,
                    AppStatusCode = result.Status.MapOperationStatus()
                }
            };
        }

        // قسمت 85 این توابع رو نوشته و توضیح داده چی هستن
        // درکل چیزی که دوست داریم بعد از انجام عملیات برای کاربر نشون داده بشه به این صورت شخصی سازی کردیم یا به قولی مپ کردیم
        protected ApiResult<TData?> CommandResult<TData>(OperationResult<TData> result,
                                                            HttpStatusCode statusCode = HttpStatusCode.OK,
                                                                                          string? locationUrl = null)
        {
            bool isSuccess = result.Status == OperationResultStatus.Success;
            if (isSuccess)
            {
                HttpContext.Response.StatusCode = (int)statusCode;
                if (!string.IsNullOrWhiteSpace(locationUrl))
                {
                    HttpContext.Response.Headers.Add("location", locationUrl);
                }
            }

            return new ApiResult<TData?>()
            {
                IsSuccess = isSuccess,
                // هست Null باشه دیتارو دریافت میکنیم درغیراینصورت میاد دیفالتو میده که یک چیزی مثل true برابر isSuccess اینجا اگر مقدار
                Data = isSuccess ? result.Data : default,
                MetaData = new MetaData()
                {
                    Message = result.Message,
                    AppStatusCode = result.Status.MapOperationStatus()
                }
            };
        }


        protected ApiResult<TData> QueryResult<TData>(TData result)
        {
            return new ApiResult<TData>()
            {
                IsSuccess = true,
                // هست Null باشه دیتارو دریافت میکنیم درغیراینصورت میاد دیفالتو میده که یک چیزی مثل true برابر isSuccess اینجا اگر مقدار
                Data = result,
                MetaData = new MetaData()
                {
                    Message = "عملیات با موفقیت انجام شد",
                    AppStatusCode = AppStatusCode.Success
                }
            };
        }
    }


    public static class EnumHelper
    {
        public static AppStatusCode MapOperationStatus(this OperationResultStatus status)
        {
            switch (status)
            {
                case OperationResultStatus.Success:
                    return AppStatusCode.Success;

                case OperationResultStatus.NotFound:
                    return AppStatusCode.NotFound;

                case OperationResultStatus.Error:
                    return AppStatusCode.LogicError;
            }

            return AppStatusCode.LogicError;
        }
    }

}
