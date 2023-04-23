using Bll.Interfaces;
using Dal.Entities;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService= eventService;
        }

        //Add event for user 
        
        [HttpPost("AddEvent")]
        public async Task< ActionResult<int>> AddEvent([FromBody]EventEntity newEvent)
        {
            try
            {
                await _eventService.AddEventAsync(newEvent);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        //get list of all events for user
        //
        [HttpGet("GetEventsForUse/{eventId}")]
        public async Task<ActionResult<List<EventEntity>>> GetEventsForUser(int usetId)
        {
            try
            {
                return Ok( await _eventService.GetEventsForUserAsync(usetId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }


        //Get an event by date

        [HttpGet("GetByDate/{date}/{userId}")]
        public async Task<ActionResult<EventEntity>> GetByDate(DateTime date, int userId)
        {
            try
            {
                return Ok( await _eventService.GetByDateAsync(date,userId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        //Delete by event id
        [HttpDelete("DeleteByEventId/{eventId}")]
        public async Task<ActionResult<int>> DeleteByEventId(int eventId)
        {
            try
            {
                await _eventService.DeleteByEventIdAsync(eventId);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }


        //Update event by event id
        [HttpPut("UpdateEvent")]
        public async Task<ActionResult<int>> UpdateEvent([FromBody]EventEntity newEvent)
        {
            try
            {
                await _eventService.UpdateEventAsync(newEvent);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
     
    }
}
