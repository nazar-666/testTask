using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using Place.Core.Data.Entites.Abstract;
using Place.Core.Repositories.Abstract;

namespace Place.Services.Services.Abstract
{
    public abstract class EntityService<TRepo, TEntity> : IEntityService<TEntity>
        where TEntity : class, IEntity
        where TRepo : IGenericRepository<TEntity>
    {
        protected EntityService(TRepo repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        private readonly TRepo _repository;
        protected TRepo Repository
        {
            get { return _repository; }
        }

        private readonly IUnitOfWork _unitOfWork;

        
        protected IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }

        #region CRUD
        public virtual TEntity Add(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            TEntity addedEntity = Repository.Add(entity);
            UnitOfWork.Commit();
            return addedEntity;
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            var result = entities.Select(entity => _repository.Add(entity)).ToList();
            UnitOfWork.Commit();
            return result;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Repository.GetAll();
        }

        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return Repository.FindBy(predicate);
        }

        public TEntity FindById(object id)
        {
            return Repository.Find(id);
        }

        public void Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            Repository.Update(entity);
            UnitOfWork.Commit();
        }

        public TEntity Delete(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            TEntity deletedEntity = Repository.Delete(entity);
            UnitOfWork.Commit();
            return deletedEntity;
        }

        public TEntity DeleteById(object id)
        {
            TEntity deletedEntity = Repository.DeleteById(id);
            UnitOfWork.Commit();
            return deletedEntity;
        }

        public DbPropertyValues GetDatabaseValues(TEntity entity)
        {
            return Repository.GetDatabaseValues(entity);
        }
        #endregion
    }
}
