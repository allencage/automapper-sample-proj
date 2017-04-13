using Repo.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Repo.Contracts
{
    public interface IMappingDecorator<TData, TDomain>
        where TData : class
        where TDomain : class
    {
        void AddDomainEntity(TDomain domainEntity);
        void UpdateDomainEntity(TDomain domainEntity);
        TDomain GetDomainEntity(long id);
        void DeleteDomainEntity(TDomain domainEntity);
        IEnumerable<TDomain> GetAllDomainEntities();
		IEnumerable<TDomain> FindAllDomainEntities(Expression<Func<TDomain, bool>> predicate);
    }
}