using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;


namespace BaseData
{
    public abstract class DBase : IDBase, IDisposable
    {
        public DBase(DbContext context)
        {
            _dataContext = context;
            _objectContext = ((IObjectContextAdapter)_dataContext).ObjectContext;
        }

        #region Variables
        private DbContext _dataContext;
        private ObjectContext _objectContext;
        #endregion

        #region Create
        public T Create<T>(T entity) where T : class
        {
            using (var context = new DbContext(_objectContext, false))
            {
                context.Set<T>().Add(entity);
                context.SaveChanges();
                return entity;
            }
        }

        public void Create<T>(List<T> entities) where T : class
        {
            using (var context = new DbContext(_objectContext, false))
            {
                context.Set<T>().AddRange(entities);
                context.SaveChanges();
            }
        }
        //Replace this with Create
        public T Insert<T>(T entity) where T : class
        {
            using (var context = new DbContext(_objectContext, false))
            {
                context.Set<T>().Add(entity);
                context.SaveChanges();
                return entity;
            }
        }
        #endregion

        #region Read
        public bool Exists<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            using (var context = new DbContext(_objectContext, false))
            {
                bool exists = context.Set<T>().AsNoTracking().Any(predicate);
                return exists;
            }
        }

        public int Count<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            using (var context = new DbContext(_objectContext, false))
            {
                int count = context.Set<T>().AsNoTracking().Count(predicate);
                return count;
            }
        }
        //Replace this with Read
        public List<T> List<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            using (var context = new DbContext(_objectContext, false))
            {
                IQueryable<T> entities = context.Set<T>().AsNoTracking().Where(predicate);
                return entities.ToList();
            }
        }
        //Replace this with Read
        public List<T> List<T>(bool sortAscending, Expression<Func<T, bool>> predicate, Expression<Func<T, int>> sortBy, int skip, int take) where T : class
        {
            using (var context = new DbContext(_objectContext, false))
            {
                IQueryable<T> entities = context.Set<T>().AsNoTracking().Where(predicate);
                entities = Sort(entities, sortBy, sortAscending);
                entities = Page(entities, skip, take);
                return entities.ToList();
            }
        }
        //Replace this with Read
        public List<T> List<T>(bool sortAscending, Expression<Func<T, bool>> predicate, Expression<Func<T, string>> sortBy, int skip, int take) where T : class
        {
            using (var context = new DbContext(_objectContext, false))
            {
                IQueryable<T> entities = context.Set<T>().AsNoTracking().Where(predicate);
                entities = Sort(entities, sortBy, sortAscending);
                entities = Page(entities, skip, take);
                return entities.ToList();
            }
        }

        public List<T> Read<T>(Expression<Func<T, bool>> predicate, string sortBy) where T : class
        {//implement sort in the future
            using (var context = new DbContext(_objectContext, false))
            {
                IQueryable<T> entities = context.Set<T>().AsNoTracking().Where(predicate);
                return entities.ToList();
            }
        }

        public T Read<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            using (var context = new DbContext(_objectContext, false))
            {
                T entity = null;
                entity = context.Set<T>().AsNoTracking().FirstOrDefault(predicate);
                return entity;
            }
        }
        //Replace this with Read
        public T Retrieve<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            using (var context = new DbContext(_objectContext, false))
            {
                T entity = null;
                entity = context.Set<T>().AsNoTracking().FirstOrDefault(predicate);
                return entity;
            }
        }
        #endregion

        #region Update
        public T Update<T>(T entity) where T : class
        {
            using (var context = new DbContext(_objectContext, false))
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
                return entity;
            }
        }
        #endregion

        #region Delete
        public void Delete<T>(T entity) where T : class
        {
            using (var context = new DbContext(_objectContext, false))
            {
                context.Entry(entity).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public void Delete<T>(List<T> entities) where T : class
        {
            using (var context = new DbContext(_objectContext, false))
            {
                context.Set<T>().RemoveRange(entities);
                context.SaveChanges();
            }
        }

        public void Delete<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            using (var context = new DbContext(_objectContext, false))
            {
                var entities = context.Set<T>().Where(predicate).ToList();
                context.Set<T>().RemoveRange(entities);
                context.SaveChanges();
            }
        }
        #endregion

        #region Other Function
        protected IQueryable<T> Sort<T>(IQueryable<T> entities, Expression<Func<T, int>> sortBy, bool sortAscending) where T : class
        {
            if (sortAscending)
            {
                entities = entities.OrderBy(sortBy);
            }
            else
            {
                entities = entities.OrderByDescending(sortBy);
            }
            return entities;
        }

        protected IQueryable<T> Sort<T>(IQueryable<T> entities, Expression<Func<T, string>> sortBy, bool sortAscending) where T : class
        {
            if (sortAscending)
            {
                entities = entities.OrderBy(sortBy);
            }
            else
            {
                entities = entities.OrderByDescending(sortBy);
            }
            return entities;
        }

        protected IQueryable<T> Page<T>(IQueryable<T> entities, int skip = 0, int take = 0) where T : class
        {
            if (take != 0)
            {
                entities = entities.Skip(skip).Take(take);
            }
            return entities;
        }

        public void Dispose()
        {
            if (_dataContext != null)
                _dataContext.Dispose();
        }
        #endregion
    }
}