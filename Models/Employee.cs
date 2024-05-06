using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleTask.Models
{
    public class Employee
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public string Mobile { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        public decimal Salary { get; set; }

        public string? ProfileImage { get; set; }
        [NotMapped]
        public IFormFile? File { get; set; }


        public int DepartmentID { get; set; }

       
        public virtual Department? Department { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? CreatedByID { get; set; }

        public virtual User? CreatedBy { get; set; }

        public int? ModifiedByID { get; set; }

        public virtual User? ModifiedBy { get; set; }
    }
}
