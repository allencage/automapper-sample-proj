using DataModels = Repo.EF.Models;
using System.Data.Entity;

namespace Repo.EF
{
	public class EmployeeRepository : Repository<DataModels.Employee>
	{
		public EmployeeRepository(DbContext dbContext) : base(dbContext)
		{
		}
	}
}
