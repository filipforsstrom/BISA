namespace BISA.Server.Services.EventService
{
    public interface IEventService
    {
        Task<ServiceResponseDTO<List<EventDTO>>> GetEvents();
        Task<ServiceResponseDTO<List<EventTypeDTO>>> GetEventTypes();
        Task<ServiceResponseDTO<EventDTO>> GetEvent(int eventId);
        Task<ServiceResponseDTO<EventDTO>> CreateEvent(EventCreateDTO eventToCreate);
        Task<ServiceResponseDTO<EventDTO>> UpdateEvent(int eventId, EventDTO eventToUpdate);
        Task<ServiceResponseDTO<EventDTO>> DeleteEvent(int eventId);
    }
}
