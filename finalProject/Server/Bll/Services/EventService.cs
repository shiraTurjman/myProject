using Bll.Interfaces;
using Dal.Entities;
using Dal.Interfaces;


namespace Bll.Functions
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _IEventRepository;
        public EventService(IEventRepository IEventRepository)
        {
            _IEventRepository = IEventRepository;
        }

        public async Task AddEventAsync(EventEntity newEvent)
        {
            await _IEventRepository.AddEventAsync(newEvent);
        }

        public async Task DeleteByEventIdAsync(int eventId)
        {
             await _IEventRepository.DeleteByEventIdAsync(eventId);
        }

        public async Task<EventEntity> GetByDateAsync(DateTime d, int userId)
        {
            return await _IEventRepository.GetByDateAsync(d,userId);
        }

        public async Task<List<EventEntity>> GetEventsForUserAsync(int userId)
        {
             return await _IEventRepository.GetEventsForUserAsync(userId);
        }

        public async Task UpdateEventAsync(EventEntity eventUp)
        {
             await _IEventRepository.UpdateEventAsync(eventUp);
        }
    }
}
