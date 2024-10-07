using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPDBConnectionDemo.Models
{
    public class Fee
    {
        [Key]
        public int FeeId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTime DueDate { get; set; }

        // Navigation properties
        public Student Student { get; set; }
    }
}