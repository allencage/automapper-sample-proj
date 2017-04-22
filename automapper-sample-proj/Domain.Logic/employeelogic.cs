using Domain.Contracts;
using Log.Contracts;
using Repo.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Domain.Logic
{
	public class EmployeeLogic : IEmployeeLogic
	{
		private readonly ICustomLog _logger;
		private readonly IMappingDecorator<IDataModel, IDomainModel> _mapping;

		public EmployeeLogic(IMappingDecorator<IDataModel, IDomainModel> mapping, ICustomLog logger)
		{
			_mapping = mapping;
			_logger = logger;
		}

		public void AddEmployee(IDomainModel domainEntity)
		{
			try
			{
				_mapping.AddDomainEntity(domainEntity);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Not able to add employee");
			}
		}

		public void UpdateEmployee(IDomainModel domainEntity)
		{
			try
			{
				_mapping.UpdateDomainEntity(domainEntity);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Not able to update employee");
				throw;
			}
		}

		public IDomainModel GetEmployee(long id)
		{
			try
			{
				var result = _mapping.GetDomainEntity(id);
				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Not able to get employee");
			}
			return null;
		}

		public IEnumerable<IDomainModel> GetAllEmployees()
		{
			try
			{
				var results = _mapping.GetAllDomainEntities();
				return results;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Not able to get all employees");
			}
			return new List<IDomainModel>();
		}

		public void DeleteEmployee(IDomainModel domainModel)
		{
			try
			{
				_mapping.DeleteDomainEntity(domainModel);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Not able to delete employee");
			}
		}

		public IEnumerable<IDomainModel> FindEmployees(Expression<Func<IDomainModel, bool>> predicate)
		{
			try
			{
				var results = _mapping.FindAllDomainEntities(predicate);
				return results;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Not able to find employee");
			}
			return new List<IDomainModel>();
		}
	}
}
