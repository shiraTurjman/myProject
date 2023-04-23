
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Dal.Entities
{
    public class ItemEntity
    {
        [Key]
        [Required]
        public int ItemId { get; set; }

        [Required]
        [ForeignKey("Categories")]
        public int CategoryId { get; set; }
        public CategoryEntity Category { get; set; }

        [Required]
        [ForeignKey("Colors")]
        public int ColorId { get; set; }
        public ColorEntity Color { get; set; }

        [Required]
        public DateTime EntryDate { get; set; }

        [ForeignKey("ImageDetails")]
        public int Img { get; set; }
        public ImageDetails ImageDetails { get; set; }

        [Required]
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public UserEntity User { get; set; }
    }
}

