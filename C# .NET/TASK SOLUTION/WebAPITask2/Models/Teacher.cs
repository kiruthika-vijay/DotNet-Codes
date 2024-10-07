using System.ComponentModel.DataAnnotations;

namespace WebAPITask2.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherID { get; set; }
        [Required]
        [StringLength(50)]
        public string? TeacherName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public DateTime HireDate { get; set; }

        // Navigation Properties
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }

}
