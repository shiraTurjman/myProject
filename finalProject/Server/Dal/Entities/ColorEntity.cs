
using System.ComponentModel.DataAnnotations;


namespace Dal.Entities
{
    public class ColorEntity
    {
        [Key]
        [Required]
        public int ColorId { get; set; }

        [Required]
        public string ColorName { get; set; }

        public string  ColorCode { get; set; }

    }
}
