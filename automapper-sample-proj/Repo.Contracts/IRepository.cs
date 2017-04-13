using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Repo.Contracts
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
        T Get(long id);
        T Get(T entity);
		IEnumerable<T> Find(Expression<Func<T,bool>> predicate);
    }
}