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
	public class EmployeeLogic : IEmployeeLogic<DomainModels.EmployeeDomainModel>
	{
		private readonly ICustomLog _logger;
		private readonly IMappingDecorator<DataModels.Employee, DomainModels.EmployeeDomainModel> _mapping;

		public EmployeeLogic(IMappingDecorator<DataModels.Employee, DomainModels.EmployeeDomainModel> mapping, ICustomLog logger)
		{
			_mapping = mapping;
			_logger = logger;
		}

		public void AddEmployee(DomainModels.EmployeeDomainModel domainEntity)
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

		public void UpdateEmployee(DomainModels.EmployeeDomainModel domainEntity)
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

		public DomainModels.EmployeeDomainModel GetEmployee(long id)
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

		public IEnumerable<DomainModels.EmployeeDomainModel> GetAllEmployees()
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
			return new List<DomainModels.EmployeeDomainModel>();
		}

		public void DeleteEmployee(DomainModels.EmployeeDomainModel domainModel)
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

		public IEnumerable<DomainModels.EmployeeDomainModel> FindEmployees(Expression<Func<DomainModels.EmployeeDomainModel, bool>> predicate)
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
			return new List<DomainModels.EmployeeDomainModel>();
		}
	}
}
