using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Dal.Models
{
    public partial class Items
    {
        public Items()
        {
            Chips = new HashSet<Chips>();
            Event = new HashSet<Event>();
            OutfitItems = new HashSet<OutfitItems>();
            TagItem = new HashSet<TagItem>();
            Uses = new HashSet<Uses>();
        }

        public int ItemId { get; set; }
        public int CategoryId { get; set; }
        public int? ColorId { get; set; }
        public DateTime EntryDate { get; set; }
        public byte[] Img { get; set; }
        public int UserId { get; set; }

        public virtual Colors Color { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<Chips> Chips { get; set; }
        public virtual ICollection<Event> Event { get; set; }
        public virtual ICollection<OutfitItems> OutfitItems { get; set; }
        public virtual ICollection<TagItem> TagItem { get; set; }
        public virtual ICollection<Uses> Uses { get; set; }
    }
}
