using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
  public  class EventEntity
    {
        public int eventId { get; set; }
        public System.DateTime dateEvent { get; set; }
        public int ?outfitId { get; set; }
        public int ?itemId { get; set; }
        public int userId { get; set; }

    }
}
