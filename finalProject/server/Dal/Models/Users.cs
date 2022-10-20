using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Dal.Models
{
    public partial class Users
    {
        public Users()
        {
            Categories = new HashSet<Categories>();
            Items = new HashSet<Items>();
            Outfits = new HashSet<OutfitsItems>();
            Tags = new HashSet<Tags>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Categories> Categories { get; set; }
        public virtual ICollection<Items> Items { get; set; }
        public virtual ICollection<OutfitsItems> Outfits { get; set; }
        public virtual ICollection<Tags> Tags { get; set; }
    }
}
