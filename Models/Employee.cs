using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Webapi.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [ForeignKey("Department")]
        public int Deptid { get; set; }

        public Department Department { get; set; }
    }
}
