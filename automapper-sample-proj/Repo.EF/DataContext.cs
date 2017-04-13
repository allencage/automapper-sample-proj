using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.EF
{
	using Models;

	public class DataContext : DbContext
	{
		public virtual DbSet<Employee> Employees { get; set; }
		public virtual DbSet<Status> Status { get; set; }
	}
}
