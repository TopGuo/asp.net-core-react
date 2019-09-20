using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using domain.repository;
using infrastructure.extensions;
using Microsoft.EntityFrameworkCore;

namespace application.services
{
    // public class DbRepository<TContext> : IDbRepository<TContext> where TContext : DbContext
    public class DbRepository<TContext> : IDbRepository where TContext : DbContext
    {
        private TContext _context;

        protected TContext DataContext
        {
            get
            {
                if (this._context == null)
                {
                    this._context = ServiceExtension.New<TContext>();
                }
                return this._context;
            }
        }
        /// <summary>
        /// add entity to context or database
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="save">save the add and all changes before this to database</param>
        public void Add(object entity, bool save = false)
        {
            this.DataContext.Add(entity);
            if (!save)
                return;
            this.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        public int Count<TSource>(Expression<Func<TSource, bool>> predicate = null) where TSource : class
        {
            if (predicate == null)
                this.DataContext.Set<TSource>().Count<TSource>();
            return this.DataContext.Set<TSource>().Count<TSource>(predicate);
        }

        /// <summary>
        /// delete entity from context or database
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="save">save the delete and all changes before this to database</param>
        public void Delete(object entity, bool save = false)
        {
            this.DataContext.Remove(entity);
            if (!save)
                return;
            this.Save();
        }

        /// <summary>
        /// delete entitys from context or database
        /// </summary>
        /// <param name="list"></param>
        /// <param name="save">save the delets and all changes before to database</param>
        public void Delete(IEnumerable<object> list, bool save = false)
        {
            this.DataContext.RemoveRange(list);
            if (!save)
                return;
            this.Save();
        }

        public bool Exists<TSource>(Expression<Func<TSource, bool>> predicate = null) where TSource : class
        {
            if (predicate == null)
                return this.DataContext.Set<TSource>().Any<TSource>();
            return this.DataContext.Set<TSource>().Any<TSource>(predicate);
        }

        /// <summary>
        /// get first or default TSource
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        public TSource First<TSource>(Expression<Func<TSource, bool>> predicate = null) where TSource : class
        {
            if (predicate == null)
                return this.DataContext.Set<TSource>().FirstOrDefault<TSource>();
            return this.DataContext.Set<TSource>().FirstOrDefault<TSource>(predicate);
        }

        public IQueryable<TSource> FromSql<TSource>(string sql, params object[] parameters) where TSource : class
        {
            return this.DataContext.Set<TSource>().FromSql<TSource>((RawSqlString)sql, parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public IQueryable<T> Pages<T>(IQueryable<T> query, int pageIndex, int pageSize, out int count) where T : class
        {
            if (pageIndex < 1)
                pageIndex = 1;
            if (pageSize < 10)
                pageSize = 10;
            count = query.Count<T>();
            query = query.Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            return query;
        }

        public IQueryable<T> Pages<T>(int pageIndex, int pageSize, out int count) where T : class
        {
            if (pageIndex < 1)
                pageIndex = 1;
            if (pageSize < 1)
                pageSize = 10;
            var source = this.DataContext.Set<T>().AsQueryable<T>();
            count = source.Count<T>();
            return source.Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
        }

        public IQueryable<T> Pages<T>(IQueryable<T> query, int pageIndex, int pageSize, out int count, out int pageCount) where T : class
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

        public IQueryable<TSource> Query<TSource>() where TSource : class
        {
            return (IQueryable<TSource>)this.DataContext.Set<TSource>();
        }

        /// <summary>
        /// save all the changes add, update, delete
        /// </summary>
        public void Save()
        {
            this.DataContext.SaveChanges();
        }

        public TSource Single<TSource>(Expression<Func<TSource, bool>> predicate = null) where TSource : class
        {
            if (predicate == null)
                return this.DataContext.Set<TSource>().SingleOrDefault<TSource>();
            return this.DataContext.Set<TSource>().SingleOrDefault<TSource>(predicate);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="save"></param>
        public void Update(object entity, bool save = false)
        {
            this.DataContext.Update(entity);
            if (!save)
                return;
            this.Save();
        }

        public void Update(IEnumerable<object> list, bool save = false)
        {
            this.DataContext.UpdateRange(list);
            if (!save)
                return;
            this.Save();
        }

        public IQueryable<TSource> Where<TSource>(Expression<Func<TSource, bool>> predicate = null) where TSource : class
        {
            if (predicate == null)
                return this.DataContext.Set<TSource>().AsQueryable<TSource>();
            return this.DataContext.Set<TSource>().Where<TSource>(predicate);
        }
    }
}