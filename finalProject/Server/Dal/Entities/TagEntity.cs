using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Dal.Entities
{
    public class TagEntity
    {
        [Key]
        [Required]
        public int TagId { get; set; }
        public string TagName { get; set; }
        [Required]
        public int UserId { get; set; }

    }
}
