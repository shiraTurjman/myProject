
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dto
{
    public class AddItemDto
    {

        public int ItemId { get; set; }

        public int CategoryId { get; set; }

        public int ColorId { get; set; }

        public DateTime EntryDate { get; set; }
        public int ImgId { get; set; }

        public int UserId { get; set; }
    }
}

