using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jwell.Framework.Paging
{
    public static class PageResultExtension
    {
        /// <summary>
        /// 当前结果集的分页信息
        /// </summary>
        /// <typeparam name="T">当前结果集类型</typeparam>
        /// <param name="source">当前结果集</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="totalCount">总页数</param>
        /// <returns></returns>
        public static PageResult<T> ToPageResult<T>(this IEnumerable<T> source, int pageIndex, int pageSize,int totalCount)
        {
            return new PageResult<T>(source, pageIndex, pageSize, totalCount);
        }
    }
}
