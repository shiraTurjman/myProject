using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Entities;
using Bll.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OutfitItemController : ControllerBase
    {
        IOutfitItemBLL bll;
        public OutfitItemController(IOutfitItemBLL _bll)
        {
            bll = _bll;
        }


    }
}
