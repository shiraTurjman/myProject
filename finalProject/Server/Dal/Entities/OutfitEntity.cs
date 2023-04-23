using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dal.Entities
{
    public class OutfitEntity
    {
        [Key]
        [Required]
        public int OutfitId { get; set; }
        public string OutfitName { get; set; }
        [Required]
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
