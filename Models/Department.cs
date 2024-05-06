using System.ComponentModel.DataAnnotations;

namespace SimpleTask.Models
{
    public class Department
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
