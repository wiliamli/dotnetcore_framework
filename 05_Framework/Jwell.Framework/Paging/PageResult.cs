using System.Collections.Generic;
using System.Linq;

namespace Jwell.Framework.Paging
{
    public class PageResult
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 总页面数
        /// </summary>
        public int TotalPages => TotalCount % PageSize <= 0 ?
            TotalCount / PageSize : TotalCount / PageSize + 1;

        /// <summary>
        /// 是否存在上一页
        /// </summary>
        public bool HasPreviousPage => PageIndex > 0;
        /// <summary>
        /// 是否存在下一页
        /// </summary>
        public bool HasNextPage => PageIndex + 1 < TotalPages;

    }

    public class PageResult<T> : PageResult
    {
        public IEnumerable<T> Pager { get; set; }

        public PageResult()
        {
            Pager = new List<T>();
        }

        /// <summary>
        /// 参数的初始化，无分页效果
        /// </summary>
        /// <param name="source">初始化集合</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="totalCount">总数量</param>
        public PageResult(IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
            : this()
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            Pager = source;
        }
    }
}
