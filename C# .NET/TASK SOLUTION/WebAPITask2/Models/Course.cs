using System.ComponentModel.DataAnnotations;

namespace WebAPITask2.Models
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }
        [Required]
        public string? CourseName { get; set; }

        // Foreign Key and Navigation Property to Teacher
        public int TeacherID { get; set; }
        public virtual Teacher? Teacher { get; set; }

        // Navigation Properties for Many-to-Many relationship with Student
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }

}
