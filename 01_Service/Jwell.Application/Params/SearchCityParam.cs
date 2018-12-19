using Jwell.Framework.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jwell.Application.Params
{
    /// <summary>
    /// 查询参数
    /// </summary>
    public class SearchCityParam: PageParam
    {
        /// <summary>
        /// 城市名称
        /// </summary>
        public string Name { get; set; }
    }
}
