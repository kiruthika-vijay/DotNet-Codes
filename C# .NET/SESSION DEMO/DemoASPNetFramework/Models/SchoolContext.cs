using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DemoASPNetFramework.Models
{
    public class SchoolContext : DbContext
    {
        public DbSet<Teachers> Teachers { get; set; }
    }
}