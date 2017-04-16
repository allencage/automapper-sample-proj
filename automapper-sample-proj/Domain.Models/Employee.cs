namespace Domain.Models
{
    public class Employee
	{
		public long Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public virtual Status Status { get; set; }
	}
}
