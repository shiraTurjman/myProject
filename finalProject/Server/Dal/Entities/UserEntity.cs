using System.ComponentModel.DataAnnotations;


namespace Dal.Entities
{
    public class UserEntity
    {
        [Key]
        [Required]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    
    }
}
