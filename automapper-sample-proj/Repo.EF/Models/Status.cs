using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repo.EF.Models
{
    public class Status
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Key, ForeignKey("Employee")]
        public long EmployeeId { get; set; }
        [Required]
        public bool IsHired { get; set; }
        [Required]
        public string ContractType { get; set; }
        [Required]
        public virtual Employee Employee { get; set; }
    }
}