using CodingChallengeTask.Enums;
using System.ComponentModel.DataAnnotations;

namespace CodingChallengeTask.Models
{
    public class TaskModel
    {
        [Key]
        public int TaskId { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public DateOnly DueDate { get; set;}
        [Required]
        public Priority Priority { get; set; }
        [Required]
        public Status Status { get; set; }
    }
}
