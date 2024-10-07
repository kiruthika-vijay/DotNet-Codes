using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ASPDBConnectionDemo.Models
{
    public class SchoolContext : DbContext
    {

        public System.Data.Entity.DbSet<ASPDBConnectionDemo.Models.Student> Students { get; set; }

        public System.Data.Entity.DbSet<ASPDBConnectionDemo.Models.Teacher> Teachers { get; set; }

        public System.Data.Entity.DbSet<ASPDBConnectionDemo.Models.Course> Courses { get; set; }

        public System.Data.Entity.DbSet<ASPDBConnectionDemo.Models.Fee> Fees { get; set; }

        public System.Data.Entity.DbSet<ASPDBConnectionDemo.Models.StudentCourse> StudentCourses { get; set; }
    }
}