

using System.ComponentModel.DataAnnotations;

namespace Dto
{
    public class LoginDto
    {
        [EmailAddress]
        [MaxLength(75)]
        [Required]
        public string Email { get; set; }

        [MaxLength(20)]
        [MinLength(5)]
        [Required]
        public string Password { get; set; }
    }
}
