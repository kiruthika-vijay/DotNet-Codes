using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPDBConnectionDemo.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        [Required]
        public string CourseName { get; set; }

        // Navigation properties
        public Teacher Teacher { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }

    }
}