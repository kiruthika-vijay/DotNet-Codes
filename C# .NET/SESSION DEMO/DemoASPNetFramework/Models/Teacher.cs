using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoASPNetFramework.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public decimal Salary { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public int ExpYears { get; set; }
    }
}