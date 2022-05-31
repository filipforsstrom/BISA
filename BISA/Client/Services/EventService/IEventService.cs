
namespace BISA.Client.Services.EventService
{
    public interface IEventService
    {
        Task<ServiceResponseViewModel<List<EventViewModel>>> GetEvents();
        Task<ServiceResponseViewModel<List<EventTypeViewModel>>> GetEventTypes();
        Task<ServiceResponseViewModel<EventViewModel>> GetEvent(int eventId);
        Task<ServiceResponseViewModel<EventViewModel>> CreateEvent(EventViewModel eventToCreate);
        Task<ServiceResponseViewModel<EventViewModel>> UpdateEvent(EventViewModel eventToUpdate);
        Task<ServiceResponseViewModel<string>> DeleteEvent(int eventId);
    }
}
