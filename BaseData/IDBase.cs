using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace BaseData
{
    public interface IDBase : IDisposable
    {
        #region Create
        T Create<T>(T entity) where T : class;
        void Create<T>(List<T> entities) where T : class;
        T Insert<T>(T entity) where T : class;
        #endregion

        #region Read
        bool Exists<T>(Expression<Func<T, bool>> predicate) where T : class;
        int Count<T>(Expression<Func<T, bool>> predicate) where T : class;
        List<T> List<T>(Expression<Func<T, bool>> predicate) where T : class;
        List<T> List<T>(bool sortAscending, Expression<Func<T, bool>> predicate, Expression<Func<T, int>> sortBy, int skip, int take) where T : class;
        List<T> List<T>(bool sortAscending, Expression<Func<T, bool>> predicate, Expression<Func<T, string>> sortBy, int skip, int take) where T : class;
        List<T> Read<T>(Expression<Func<T, bool>> predicate, string sortBy) where T : class;
        T Read<T>(Expression<Func<T, bool>> predicate) where T : class;
        T Retrieve<T>(Expression<Func<T, bool>> predicate) where T : class;
        #endregion

        #region Update
        T Update<T>(T entity) where T : class;
        #endregion

        #region Delete
        void Delete<T>(T entity) where T : class;
        void Delete<T>(List<T> entities) where T : class;
        void Delete<T>(Expression<Func<T, bool>> predicate) where T : class;
        #endregion

        #region Other Function
        #endregion
    }
}