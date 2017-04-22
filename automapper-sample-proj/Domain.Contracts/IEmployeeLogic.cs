using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Domain.Contracts
{
	public interface IEmployeeLogic
	{
		void AddEmployee(IDomainModel domainEntity);
		void UpdateEmployee(IDomainModel domainEntity);
		IDomainModel GetEmployee(long id);
		IEnumerable<IDomainModel> GetAllEmployees();
		void DeleteEmployee(IDomainModel employee);
		IEnumerable<IDomainModel> FindEmployees(Expression<Func<IDomainModel, bool>> predicate);
	}
}
