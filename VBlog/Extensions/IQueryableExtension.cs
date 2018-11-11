using NPoco;
using NPoco.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace VBlog.Extensions
{
    public static class IQueryableExtension
    {
        public static IQueryProvider<TSource> HasWhere<TSource>(this IQueryProvider<TSource> query, object target,
            Expression<Func<TSource, bool>> whExpression)
        {
            if (target != null)
            {
                query = query.Where(whExpression);
            }
            return query;
        }
        /// <summary>
        /// 转化page
        /// </summary>
        /// <typeparam name="TEntity">转化前</typeparam>
        /// <typeparam name="TView">转化后</typeparam>
        /// <param name="page"></param>
        /// <returns></returns>
        public static Page<TView> ToViewPage<TEntity, TView>(this Page<TEntity> page, Func<TEntity, TView> func)
        {
            var view = new Page<TView>
            {
                CurrentPage = page.CurrentPage,
                ItemsPerPage = page.ItemsPerPage,
                TotalItems = page.TotalItems,
                TotalPages = page.TotalPages,
                Items = null
            };
            if (page.Items != null && page.Items.Count > 0)
                view.Items = page.Items.Select(func).ToList();
            return view;
        }
    }
}
