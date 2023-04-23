using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Dal.Entities
{
    public class UseEntity
    {
        [Key]
        [Required]
        public int UseId { get; set; }

        [Required]
        [ForeignKey("Items")]
        public int ItemId { get; set; }
        public ItemEntity Item { get; set; }
        public DateTime DateUse { get; set; }
    }
}
