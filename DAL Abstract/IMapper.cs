using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Abstract
{
    public interface IMapper<TEntity, TEntityDTO, Repo>
       where TEntity : class, IEntity
       where TEntityDTO : class
       where Repo : class
    {
        TEntityDTO Map(TEntity entity);

        TEntity DeMap(TEntityDTO dto);
    }
}
