using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
   public  class UserEntity
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string address { get; set; }
    }
}
