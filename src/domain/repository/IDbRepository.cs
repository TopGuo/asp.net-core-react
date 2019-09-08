using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace domain.repository
{
    // public interface IDbRepository<TContext> where TContext : DbContext
    public interface IDbRepository
    {
        /// <summary>
        /// get <see cref="TSource" /> from raw sql query
        /// the TSource must in database or <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IQueryable<TSource> FromSql<TSource>(string sql, params object[] parameters) where TSource : class;

        /// <summary>get single or default TSource</summary>
        /// <typeparam name="TSource">entity</typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TSource Single<TSource>(Expression<Func<TSource, bool>> predicate = null) where TSource : class;

        /// <summary>get first or default TSource</summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TSource First<TSource>(Expression<Func<TSource, bool>> predicate = null) where TSource : class;

        /// <summary>select entity by conditions</summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<TSource> Where<TSource>(Expression<Func<TSource, bool>> predicate = null) where TSource : class;

        /// <summary>counting the entity's count under this condition</summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count<TSource>(Expression<Func<TSource, bool>> predicate = null) where TSource : class;

        /// <summary>return the query</summary>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        IQueryable<TSource> Query<TSource>() where TSource : class;

        /// <summary>check the condition</summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Exists<TSource>(Expression<Func<TSource, bool>> predicate = null) where TSource : class;

        /// <summary>paging the query</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageIndex">page index</param>
        /// <param name="pageSize">page size </param>
        /// <param name="count">total row record count</param>
        /// <returns></returns>
        IQueryable<T> Pages<T>(IQueryable<T> query, int pageIndex, int pageSize, out int count) where T : class;

        /// <summary>paging the query</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageIndex">page index</param>
        /// <param name="pageSize">page size </param>
        /// <param name="count">total row record count</param>
        /// <returns></returns>
        IQueryable<T> Pages<T>(int pageIndex, int pageSize, out int count) where T : class;

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count">总条数</param>
        /// <param name="pageCount">页数</param>
        /// <returns></returns>
        IQueryable<T> Pages<T>(IQueryable<T> query, int pageIndex, int pageSize, out int count, out int pageCount) where T : class;
        /// <summary>save all the changes add, update, delete</summary>
        void Save();

        /// <summary>add entity to context or database</summary>
        /// <param name="entity"></param>
        /// <param name="save">save the add and all changes before this to database</param>
        void Add(object entity, bool save = false);

        /// <summary>update entity to context or database</summary>
        /// <param name="entity"></param>
        /// <param name="save">save the update and all changes before this to database</param>
        void Update(object entity, bool save = false);

        /// <summary>update entitys to context or database</summary>
        /// <param name="list"></param>
        /// <param name="save">save the updates and all changes before this to database</param>
        void Update(IEnumerable<object> list, bool save = false);

        /// <summary>delete entity from context or database</summary>
        /// <param name="entity"></param>
        /// <param name="save">save the delete and all changes before this to database</param>
        void Delete(object entity, bool save = false);

        /// <summary>delete entitys from context or database</summary>
        /// <param name="list"></param>
        /// <param name="save">save the deletes and all changes before this to database</param>
        void Delete(IEnumerable<object> list, bool save = false);
    }
}