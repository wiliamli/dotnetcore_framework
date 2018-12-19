using Jwell.Framework.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Framework.Mvc
{
    public class StandardJsonResult : ActionResult, IStandardResult
    {
        public bool Success { get ; set; }

        public string Message { get ; set; }

        public string StatusCode { get; set; }

        public string ContentType { get; set; }

        public StandardJsonResult()
        {
            ContentType = "application/json";
        }

        public void StandardAction(Action action)
        {
            try
            {
                action();
                Success = true;
            }
            catch (Exception ex)
            {
                Success = false;
                throw ex;
            }
        }

        public override void ExecuteResult(ActionContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = ContentType;
            response.WriteAsync(Serializer.ToJson(ToJsonObject()), Encoding.UTF8);
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = ContentType;
            return response.WriteAsync(Serializer.ToJson(ToJsonObject()), Encoding.UTF8);
        }

        protected virtual IStandardResult ToJsonObject()
        {
            return new StandardResult
            {
                Success = Success,
                Message = Message
            };
        }
    }

    public class StandardJsonResult<T> : StandardJsonResult, IStandardResult<T>
    {
        public T Data { get; set; }

        protected override IStandardResult ToJsonObject()
        {
            return new StandardResult<T>
            {
                Success = Success,
                Message = Message,
                Data = Data
            };
        }
    }
}
