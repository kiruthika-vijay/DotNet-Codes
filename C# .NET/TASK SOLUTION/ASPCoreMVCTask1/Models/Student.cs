namespace ASPCoreMVCTask1.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set;}
        public string Phone { get; set;}
        public DateTime DOB { get; set;}

        // Navigation properties
        public ICollection<Course> Courses { get; set; }

    }
}
