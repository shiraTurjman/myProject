using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Dal.Models
{
    public partial class Tags
    {
        public Tags()
        {
            TagItem = new HashSet<TagItem>();
        }

        public int TagId { get; set; }
        public string TagName { get; set; }
        public int UserId { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<TagItem> TagItem { get; set; }
    }
}
