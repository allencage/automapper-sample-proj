using System.Data.Entity;

namespace Repo.EF
{
	using Models;

	public class DataContext : DbContext
	{
		public DataContext() : base("EmployeesDatabase")
		{

		}
		public virtual DbSet<Employee> Employees { get; set; }
		public virtual DbSet<Status> Status { get; set; }
	}
}
