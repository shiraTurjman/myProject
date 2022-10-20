using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bll.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        IEventBLL bll;
        public EventController(IEventBLL _bll)
        {
            bll = _bll;
        }

        //Add event for user 
        
        [HttpPost("AddEvent")]
        public ActionResult<int> AddEvent([FromBody]EventEntity newEvent)
        {
            return Ok(bll.AddEventAsync(newEvent));
        }

        //get list of all events for user
        //
        [HttpGet("GetEventsForUse/{eventId}")]
        public ActionResult<List<EventEntity>> GetEventsForUser(int usetId)
        {
            return Ok(bll.GetEventsForUserAsync(usetId));
        }


        //Get an event by date

        [HttpGet("GetByDate/{d}")]
        public ActionResult<EventEntity> GetByDate(DateTime d)
        {
            return Ok(bll.GetByDateAsync(d));
        }

        //Delete by event id
        [HttpDelete("DeleteByEventId/{eventId}")]
        public ActionResult<int> DeleteByEventId(int eventId)
        {
            return Ok(bll.DeleteByEventIdAsync(eventId));
        }


        //Update event by event id
        [HttpPut("UpdateEvent")]
        public ActionResult<int> UpdateEvent([FromBody]EventEntity newEvent)
        {
            return Ok(bll.UpdateEventAsync(newEvent));
        }
     
    }
}
