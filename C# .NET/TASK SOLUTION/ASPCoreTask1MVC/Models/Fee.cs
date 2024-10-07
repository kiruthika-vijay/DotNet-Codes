using System.ComponentModel.DataAnnotations;

namespace ASPCoreTask1MVC.Models
{
    public class Fee
    {
        [Key]
        public int FeeId { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public DateOnly DueDate { get; set; }
        [Required]
        public int StudentId { get; set; }

        // Navigation properties
        public virtual Student? Student { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
