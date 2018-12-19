using Jwell.Framework.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jwell.Web.App_Start
{
    /// <summary>
    /// 全局异常过滤
    /// </summary>
    public class GlobalExceptionFilterAttribute: ExceptionFilterAttribute
    {
        /// <summary>
        /// 异常捕获
        /// </summary>
        /// <param name="context">当前上下文</param>
        public override void OnException(ExceptionContext context)
        {
            //TODO: 这里可以记录异常到日志系统，以下这些等都可以作为异常日志信息

            //context.HttpContext.Request.Query; //获取当前的请求参数
            //context.HttpContext.Request.Path.ToUriComponent(); //获取当前的请求URL
            //context.HttpContext.Request.Method; //获取当前的请求方法
            //context.Exception.Message; //当前的异常信息

            context.Result = new StandardJsonResult()
            {
                Message = context.Exception.Message,
                StatusCode = context.HttpContext.Response.StatusCode.ToString(),
                Success = false
            };
        }
    }
}
