namespace BISA.Server.Services.EventService
{
    public interface IEventService
    {
        Task<List<EventDTO>> GetEvents();
        Task<List<EventTypeDTO>> GetEventTypes();
        Task<EventDTO> GetEvent(int eventId);
        Task<EventDTO> CreateEvent(EventCreateDTO eventToCreate);
        Task<EventDTO> UpdateEvent(int eventId, EventDTO eventToUpdate);
        Task<bool> DeleteEvent(int eventId);
    }
}
