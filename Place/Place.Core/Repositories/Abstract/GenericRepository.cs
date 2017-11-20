using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using Place.Core.Data;
using Place.Core.Data.Entites.Abstract;

namespace Place.Core.Repositories.Abstract
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DbSet<TEntity> _dbSet;
        protected DbSet<TEntity> DbSet
        {
            get { return _dbSet; }
        }
        protected readonly ApplicationDbContext Context;

        protected GenericRepository(ApplicationDbContext сontext)
        {
            Context = сontext;
            if (Context != null)
            {
                _dbSet = Context.Set<TEntity>();
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DbSet.AsEnumerable();
        }

        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).AsEnumerable();
        }

        public TEntity Find(object id)
        {
            return DbSet.Find(id);
        }

        public TEntity Add(TEntity entity)
        {
            return DbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public TEntity Delete(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            return DbSet.Remove(entity);
        }

        public TEntity DeleteById(object id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            return Delete(entityToDelete);
        }

        public DbPropertyValues GetDatabaseValues(TEntity entity)
        {
            return Context.Entry(entity).GetDatabaseValues();
        }

        public IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        #region IDisposable

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            _disposed = true;
        }

        #endregion
    }
}
