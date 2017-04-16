using Domain.Contracts;
using Log.Contracts;
using Repo.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DataModels = Repo.EF.Models;
using DomainModels = Domain.Models;

namespace Domain.Logic
{
	public class EmployeeLogic : IEmployeeLogic<DomainModels.Employee>
	{
		private readonly ICustomLog _logger;
		private readonly IMappingDecorator<DataModels.Employee, DomainModels.Employee> _mapping;
		private EmployeeLogic(IMappingDecorator<DataModels.Employee, DomainModels.Employee> mapping, ICustomLog logger)
		{
			_mapping = mapping;
			_logger = logger;
		}

		public static EmployeeLogic CreateLogicWithMappingDecoratorAndLogger
			(IMappingDecorator<DataModels.Employee, DomainModels.Employee> mapping, ICustomLog logger)
		{
			return new EmployeeLogic(mapping, logger);
		}

		public void AddEmployee(DomainModels.Employee domainEntity)
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

		public void UpdateEmployee(DomainModels.Employee domainEntity)
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

		public DomainModels.Employee GetEmployee(long id)
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

		public IEnumerable<DomainModels.Employee> GetAllEmployees()
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
			return new List<DomainModels.Employee>();
		}

		public void DeleteEmployee(DomainModels.Employee employee)
		{
			try
			{
				_mapping.DeleteDomainEntity(employee);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Not able to delete employee");
			}
		}

		public IEnumerable<DomainModels.Employee> FindEmployees(Expression<Func<DomainModels.Employee, bool>> predicate)
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
			return new List<DomainModels.Employee>();
		}
	}
}
