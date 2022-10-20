using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Dal.Models
{
    public partial class Chips
    {
        public int ChipId { get; set; }
        public int ItemId { get; set; }
        public string ItemLocation { get; set; }

        public virtual Items Item { get; set; }
    }
}
