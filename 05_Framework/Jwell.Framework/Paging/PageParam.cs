using System;
using System.Collections.Generic;
using System.Text;

namespace Jwell.Framework.Paging
{
    /// <summary>
    /// 分页基类
    /// </summary>
    public abstract class PageParam
    {
        /// <summary>
        /// 分页序号
        /// </summary>
        public virtual int PageIndex { get; set; }

        /// <summary>
        /// 分页大小
        /// </summary>
        public virtual int PageSize { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public virtual string FieldSort { get; set; }
    }
}
