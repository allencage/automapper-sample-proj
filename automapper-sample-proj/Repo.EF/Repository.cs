using Repo.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Repo.EF
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbContext _dbContext;
        private IDbSet<T> _entities;
        public IDbSet<T> Entities => _entities ?? (_entities = _dbContext.Set<T>());

        private Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public static Repository<T> CreateRepositoryWithDbContext(DbContext dbContext)
        {
            return new Repository<T>(dbContext);
        }
        public void Add(T entity)
        {
            Entities.Add(entity);
        }

        public void Update(T entity)
        {
            Entities.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            Entities.Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            var results = Entities.ToList();
            return results;
        }

        public T Get(long id)
        {
            var result = Entities.Find(id);
            return result;
        }

        public T Get(T entity)
        {
            var primaryKey = GetPrimaryKeyUsingReflection(entity);
            if (primaryKey != null)
                return Get((long)primaryKey.GetValue(entity));
            return null;
        }

		public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
		{
			var result = Entities.Where(predicate);
			return result;
		}

        private System.Reflection.PropertyInfo GetPrimaryKeyUsingReflection(T entity)
        {
            var key = entity.GetType().GetProperties().FirstOrDefault(p => p.CustomAttributes.Any(attr => attr.AttributeType == typeof(KeyAttribute)));
            return key;
        }
    }
}
