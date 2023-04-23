using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dal.Entities
{
    public class EventEntity
    {
        [Key]
        [Required]
        public int EventId { get; set; }

        [Required]
        public DateTime DateEvent { get; set; }

        [Required]
        public int OutfitId { get; set; }

        [Required]
        [ForeignKey("Items")]
        public int ItemId { get; set; }
        public ItemEntity Item { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
