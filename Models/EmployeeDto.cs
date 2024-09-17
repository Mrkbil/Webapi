using System.ComponentModel.DataAnnotations;

namespace Webapi.Models
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string DepartmentName { get; set; }
    }

    public class EmployeeCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public int Deptid { get; set; }
    }

}
