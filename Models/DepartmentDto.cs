using System.ComponentModel.DataAnnotations;

namespace Webapi.Models
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class DepartmentCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }

}
