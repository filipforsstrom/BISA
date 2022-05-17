﻿
namespace BISA.Client.Services.EventService
{
    public interface IEventService
    {
        Task<List<EventViewModel>> GetEvents();
        Task<List<EventTypeViewModel>> GetEventTypes();
        Task<EventViewModel> GetEvent(int id);
        Task<EventViewModel> CreateEvent(EventViewModel eventToCreate);
        Task<EventViewModel> UpdateEvent(EventViewModel eventToUpdate);
        Task<EventViewModel> DeleteEvent(int id);
    }
}
