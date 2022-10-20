using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bll.Interfaces;
using Entities.Entities;
namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OutfitsController : ControllerBase
    {
        IOutfitsBLL bll;
        public OutfitsController(IOutfitsBLL _bll)
        {
            bll = _bll;

        }

    }
}
