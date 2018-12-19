using Jwell.Framework.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jwell.Application.Params
{
    /// <summary>
    /// 查询参数
    /// </summary>
    public class SearchProvinceParam: PageParam
    {
        /// <summary>
        /// 省份名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 国家编码
        /// </summary>
        public string CountryCode { get; set; }
    }
}
