using CodingChallengeTask.Enums;
using System.ComponentModel.DataAnnotations;

namespace CodingChallengeTask.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [StringLength(50)]
        public string? Email { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(50)]
        public string? Password { get; set; }
        [Required]
        [StringLength(50)]
        public Role Role { get; set; }

    }
}
