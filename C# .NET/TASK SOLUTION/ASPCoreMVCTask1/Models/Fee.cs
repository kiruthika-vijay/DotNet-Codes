namespace ASPCoreMVCTask1.Models
{
    public class Fee
    {
        public int FeeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public int StudentId { get; set; }

        // Navigation properties
        public Student Student { get; set; }
    }
}
