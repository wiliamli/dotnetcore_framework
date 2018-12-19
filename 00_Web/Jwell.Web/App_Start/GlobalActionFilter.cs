using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jwell.Web.App_Start
{
    /// <summary>
    /// ActionFilter
    /// </summary>
    public class GlobalActionFilter : IActionFilter
    {
        /// <summary>
        /// Action执行执行之前
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //customize
        }

        /// <summary>
        /// Action执行完成后
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //customize
        } 
    }
}
