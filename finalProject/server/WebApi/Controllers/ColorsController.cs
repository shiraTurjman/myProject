using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bll.Interfaces;
using Entities.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
          
        IColorsBLL bll;
        public ColorsController(IColorsBLL _bll)
        {
            bll = _bll;
        }
        [HttpGet("GetAll")]
        public ActionResult<List<ColorEntity>> getAll()
        {
            return Ok(bll.getAllAsync());
        }
    }
}
