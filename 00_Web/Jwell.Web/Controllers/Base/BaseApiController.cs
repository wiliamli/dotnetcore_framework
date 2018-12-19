using Jwell.Framework.Mvc;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Jwell.Web.Controllers.Base
{   
    /// <summary>
    /// 基类Api
    /// </summary>
    public class BaseApiController : ControllerBase
    {
        /// <summary>
        /// 标准数据返回
        /// </summary>
        /// <param name="action">action</param>
        /// <returns></returns>
        protected StandardJsonResult StandardAction(Action action)
        {
            var result = new StandardJsonResult();
            result.StandardAction(action);
            return result;
        }

        /// <summary>
        /// 标准数据返回
        /// </summary>
        /// <typeparam name="T">返回参数</typeparam>
        /// <param name="func">func</param>
        /// <returns></returns>
        protected StandardJsonResult<T> StandardAction<T>(Func<T> func)
        {
            var result = new StandardJsonResult<T>();
            result.StandardAction(() =>
            {
                result.Data = func();
            });
            return result;
        }
    }
}
