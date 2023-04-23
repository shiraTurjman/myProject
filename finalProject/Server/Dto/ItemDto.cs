

namespace Dto
{
    public class ItemDto
    {
        public int ItemId { get; set; }

        public int CategoryId { get; set; }
        public int ColorId { get; set; }

        public DateTime EntryDate { get; set; }

        // public int ImgId { get; set; }
        public byte[] ImgData { get; set; } 
        public int UserId { get; set; }

    }
}
