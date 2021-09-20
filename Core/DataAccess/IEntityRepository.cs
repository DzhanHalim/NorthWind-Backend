using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess 
{
    // generic constraint
    // class : reference type
    // T is IEntity or a class that implements IEntity
    // new()= can not be an abstract class(interface)
    public interface IEntityRepository<T>where T:class,IEntity,new()
    {
       
        List<T> GetAll(Expression<Func<T,bool>> filter=null);
        T Get(Expression<Func<T,bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        
    }
}
