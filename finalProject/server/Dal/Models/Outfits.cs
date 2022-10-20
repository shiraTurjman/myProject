using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Dal.Models
{
    public partial class Outfits
    {
        public Outfits()
        {
            Event = new HashSet<Event>();
            OutfitItems = new HashSet<OutfitItems>();
        }

        public int OutfitId { get; set; }
        public string OutfitName { get; set; }
        public int UserId { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Event> Event { get; set; }
        public virtual ICollection<OutfitItems> OutfitItems { get; set; }
    }
}
