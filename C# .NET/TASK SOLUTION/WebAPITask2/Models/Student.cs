using System.ComponentModel.DataAnnotations;

namespace WebAPITask2.Models
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }
        [Required]
        [StringLength(50)]
        public string? StudentName { get; set; }
        [Required]
        [Range(5,18)]
        public int Age { get; set; }
        [Required]
        public DateTime AdmissionDate { get; set; }


        // Navigation Properties
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
        public ICollection<Fee> Fees { get; set; } = new List<Fee>();
    }

}
