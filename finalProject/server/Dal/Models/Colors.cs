using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Dal.Models
{
    public partial class Colors
    {
        public Colors()
        {
            Items = new HashSet<Items>();
        }

        public int ColorId { get; set; }
        public string ColorName { get; set; }

        public virtual ICollection<Items> Items { get; set; }
    }
}
