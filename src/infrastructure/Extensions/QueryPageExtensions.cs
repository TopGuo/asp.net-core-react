using System.Linq;

namespace infrastructure.extensions
{
    public static class QueryPageExtensions
    {
        /// <summary>
		/// query paging
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="query">query</param>
		/// <param name="pageIndex">current page index</param>
		/// <param name="pageSize">page size, if size than 100 then 100</param>
		/// <param name="count">out the total records count</param>
		/// <returns></returns>
		public static IQueryable<T> Pages<T>(this IQueryable<T> query, int pageIndex, int pageSize, out int count) where T : class
        {
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            if (pageSize < 1)
            {
                pageSize = 10;
            }
            if (pageSize > 100)
            {
                pageSize = 100;
            }
            count = query.Count();
            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return query;
        }

        /// <summary>
        /// query paging
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">query</param>
        /// <param name="pageIndex">current page index</param>
        /// <param name="pageSize">page size, if size than 100 then 100</param>
        /// <param name="count">out the total records count</param>
        /// <param name="pageCount">total page count</param>
        /// <returns></returns>
        public static IQueryable<T> Pages<T>(this IQueryable<T> query, int pageIndex, int pageSize, out int count, out int pageCount) where T : class
        {
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            if (pageSize < 1)
            {
                pageSize = 10;
            }
            if (pageSize > 100)
            {
                pageSize = 100;
            }
            count = query.Count();
            pageCount = count / pageSize;
            if ((decimal)pageCount < (decimal)count / (decimal)pageSize)
            {
                pageCount++;
            }
            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return query;
        }
    }
}