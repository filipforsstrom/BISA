namespace BISA.Server.Services.EventService
{
    public interface IEventService
    {
        Task<ServiceResponseDTO<List<EventDTO>>> GetEvents();
        Task<ServiceResponseDTO<List<EventTypeDTO>>> GetEventTypes();
        Task<ServiceResponseDTO<EventDTO>> GetEvent(int id);
        Task<ServiceResponseDTO<EventDTO>> CreateEvent(EventDTO eventToCreate);
        Task<ServiceResponseDTO<EventDTO>> UpdateEvent(EventDTO eventToUpdate);
        Task<ServiceResponseDTO<EventDTO>> DeleteEvent(int id);
    }
}
