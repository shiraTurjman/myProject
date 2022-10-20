using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class ItemEntity
    {
        public int itemId { get; set; }
        public int categoryId { get; set; }
        public int? color { get; set; }
        public System.DateTime entryDate { get; set; }
        public byte[] img { get; set; }
        public int userId { get; set; }
    }
}
