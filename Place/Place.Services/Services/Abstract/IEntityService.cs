﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using Place.Core.Data.Entites.Abstract;

namespace Place.Services.Services.Abstract
{
    public interface IEntityService<TEntity> : IService where TEntity : IEntity
    {
        TEntity Add(TEntity entity);
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        TEntity FindById(object id);
        void Update(TEntity entity);
        TEntity Delete(TEntity entity);
        TEntity DeleteById(object id);
        DbPropertyValues GetDatabaseValues(TEntity entity);
    }
}
