namespace ASPCoreMVCTask1.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int TeacherId { get; set; }

        // Navigation properties
        public Teacher Teacher { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
