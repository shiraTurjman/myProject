using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Entities
{
    public class ImageDetails
    {

        [Key]
        public int ID { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
    }
}

