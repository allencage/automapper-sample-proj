using AutoMapper;
using Repo.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Repo.Mapping
{
    public class MappingDecorator<TData, TDomain> : IMappingDecorator<TData, TDomain> 
        where TData : class 
        where TDomain : class
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TData> _repo;

        public MappingDecorator(IMapper mapper, IRepository<TData> repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public static MappingDecorator<TData,TDomain> CreateMappingDecoratorWithMapperAndRepo(IMapper mapper, IRepository<TData> repo)
        {
            return new MappingDecorator<TData, TDomain>(mapper, repo);
        }

        public void AddDomainEntity(TDomain domainEntity)
        {
            var mappedDataEntity = _mapper.Map<TData>(domainEntity);
            _repo.Add(mappedDataEntity);
        }

        public void UpdateDomainEntity(TDomain domainEntity)
        {
            var mappedDataValues = _mapper.Map<TData>(domainEntity);
            var entityInTheRepo = _repo.Get(mappedDataValues);
            var dataToBeUpdated = _mapper.Map(domainEntity, entityInTheRepo);
            _repo.Update(dataToBeUpdated);
        }

        public TDomain GetDomainEntity(long id)
        {
            var dataEntity = _repo.Get(id);
            var domainEntity = _mapper.Map<TDomain>(dataEntity);
            return domainEntity;
        }

        public void DeleteDomainEntity(TDomain domainEntity)
        {
            var mappedDataEntity = _mapper.Map<TData>(domainEntity);
            var entityInTheRepo = _repo.Get(mappedDataEntity);
            _repo.Delete(entityInTheRepo);
        }

        public IEnumerable<TDomain> GetAllDomainEntities()
        {
            var dataEntities = _repo.GetAll();
            var domainEntities = _mapper.Map<IEnumerable<TDomain>>(dataEntities);
            return domainEntities;
        }

		public IEnumerable<TDomain> FindAllDomainEntities(Expression<Func<TDomain, bool>> predicate)
		{
			var mappedPredicate = _mapper.Map<Expression<Func<TData, bool>>>(predicate);
			var repoResults = _repo.Find(mappedPredicate);
			var domainMappedResults = _mapper.Map<IEnumerable<TDomain>>(repoResults);
			return domainMappedResults;
		}
    }
}
