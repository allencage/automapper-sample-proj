using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Domain.Contracts
{
	public interface IEmployeeLogic<T> where T : class
	{
		void AddEmployee(T domainEntity);
		void UpdateEmployee(T domainEntity);
		T GetEmployee(long id);
		IEnumerable<T> GetAllEmployees();
		void DeleteEmployee(T employee);
		IEnumerable<T> FindEmployees(Expression<Func<T, bool>> predicate);
	}
}
