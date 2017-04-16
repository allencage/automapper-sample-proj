namespace Domain.Models
{
	public class Status
	{
		public long Id { get; set; }
        public long Employee_Id { get; set; }
        public bool IsHired { get; set; }
        public string ContractType { get; set; }
    }
}