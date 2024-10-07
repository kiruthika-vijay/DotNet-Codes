using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPWebAPITask1.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        [Required]
        public string? CourseName { get; set; }
        [Required]
        public string? CourseCode { get; set; }
        [Required]
        [ForeignKey("Teacher")]
        public int? TeacherId { get; set; }

        // Navigation properties
        public virtual Teacher? Teacher { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();

    }
}
