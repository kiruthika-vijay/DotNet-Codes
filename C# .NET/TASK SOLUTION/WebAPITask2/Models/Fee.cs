using System.ComponentModel.DataAnnotations;

namespace WebAPITask2.Models
{
    public class Fee
    {
        [Key]
        public int FeeID { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }

        // Foreign Key and Navigation Property to Student
        public int StudentID { get; set; }
        public virtual Student? Student { get; set; }
    }

}
