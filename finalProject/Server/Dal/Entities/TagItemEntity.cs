using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dal.Entities
{
    public class TagItemEntity
    {
        [Key]
        [Required]
        public int TagItemId { get; set; }

        [Required]
        [ForeignKey("Tags")]
        public int TagId { get; set; }
        public TagEntity Tag { get; set; }

        [Required]
        [ForeignKey("Items")]
        public int ItemId { get; set; }
        public ItemEntity Item { get; set; }
    }
}
