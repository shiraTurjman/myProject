using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Dal.Models
{
    public partial class OutfitItems
    {
        public int OutfitItemId { get; set; }
        public int ItemId { get; set; }
        public int OutfitId { get; set; }

        public virtual Items Item { get; set; }
        public virtual OutfitsItems Outfit { get; set; }
    }
}
