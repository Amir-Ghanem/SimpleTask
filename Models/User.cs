using System.ComponentModel.DataAnnotations;

namespace SimpleTask.Models
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public virtual List<Employee> CreatedEmployees { get; set; } = new List<Employee>();

        public virtual List<Employee> ModifiedEmployees { get; set; } = new List<Employee>();
    }
}
