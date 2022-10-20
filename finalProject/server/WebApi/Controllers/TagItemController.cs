using Bll.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagItemController : ControllerBase
    {
        ITagItemBLL bll;
        public TagItemController(ITagItemBLL _bll)
        {
            bll = _bll;
        }

    }
}
