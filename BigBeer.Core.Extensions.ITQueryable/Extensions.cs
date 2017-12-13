using System;
using System.Collections.Generic;
using System.Linq;

namespace BigBeer.Core.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourse">数据源</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="mark">备注:通常用于数据统计</param>
        /// <returns></returns>
        public static Page<T> ToPage<T>(this IQueryable<T> sourse, int pageIndex, int pageSize, object mark = null)
        {
            var TotalCount = sourse.Count();
            var TotalPage = (int)Math.Ceiling(TotalCount / (double)pageSize);
            PageSize page = new PageSize()
            {
                TotalCount = sourse.Count(),
                TotalPage = TotalPage,
                LastPage = TotalPage,
                NextPage = pageIndex >= TotalPage ? pageIndex : pageIndex + 1,
                NowPage = pageIndex,
                PriePage = pageIndex <= 1 ? pageIndex : pageIndex - 1
            };
            return new Page<T>()
            {
                Data = sourse.Skip((pageIndex - 1) * pageSize).Take(pageSize),
                Pageed = page,
                Mark = mark
            };
        }
        /// <summary>
        /// 转换为动态对象的分页
        /// </summary>
        /// <typeparam name="T">输入对象</typeparam>
        /// <param name="sourse">输入对象</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页数据量</param>
        /// <param name="dataCount">总数据量</param>
        /// <param name="filter">过滤函数</param>
        /// <param name="mark">附加数据</param>
        /// <returns></returns>
        public static Page<dynamic> ToDynamicPage<T>(this IQueryable<T> sourse, int pageIndex, int pageSize, int dataCount, Func<IList<T>, IQueryable<dynamic>> filter, object mark = null)
        {
            var TotalCount = dataCount;
            var TotalPage = (int)Math.Ceiling(TotalCount / (double)pageSize);
            PageSize page = new PageSize()
            {
                TotalCount = TotalCount,
                TotalPage = TotalPage,
                LastPage = TotalPage,
                NextPage = pageIndex >= TotalPage ? pageIndex : pageIndex + 1,
                NowPage = pageIndex,
                PriePage = pageIndex <= 1 ? pageIndex : pageIndex - 1
            };
            var data = sourse.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            var pageData = filter(data);
            return new Page<dynamic>()
            {
                Data = pageData,
                Pageed = page,
                Mark = mark
            };
        }
    }
    public class Page<T>
    {
        public Page()
        {

        }
        public virtual IQueryable<T> Data { get; set; }
        public virtual PageSize Pageed { get; set; }

        public virtual object Mark { get; set; }
    }
    /// <summary>
    /// 页码
    /// </summary>
    public class PageSize
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public virtual int TotalPage { get; set; }
        /// <summary>
        /// 下一页
        /// </summary>
        public virtual int NextPage { get; set; }
        /// <summary>
        /// 上一页
        /// </summary>
        public virtual int PriePage { get; set; }
        /// <summary>
        /// 尾页
        /// </summary>
        public virtual int LastPage { get; set; }
        /// <summary>
        /// 总数据
        /// </summary>
        public virtual int TotalCount { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public virtual int NowPage { get; set; }
    }
}