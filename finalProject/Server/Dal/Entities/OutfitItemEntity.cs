using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dal.Entities
{
    public class OutfitItemEntity
    {
        [Key]
        [Required]
        public int OutfitItemId { get; set; }
        [Required]
        public int ItemId { get; set; }

        [Required]
        public int OutfitId { get; set; }
    }
}
