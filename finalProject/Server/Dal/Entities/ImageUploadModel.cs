using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Entities
{
    public class ImageUploadModel
    {
        public IFormFile ImageDetails { get; set; }
    }
}
