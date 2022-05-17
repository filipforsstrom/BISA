namespace BISA.Client.Services.EventService
{
    public class EventService : IEventService
    {
        private readonly HttpClient _http;

        public EventService(HttpClient http)
        {
            _http = http;
        }
        public Task<EventViewModel> CreateEvent(EventViewModel eventToCreate)
        {
            throw new NotImplementedException();
        }

        public Task<EventViewModel> DeleteEvent(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<EventViewModel> GetEvent(int id)
        {
            var response = await _http.GetAsync($"api/events/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EventViewModel>();
            }
            else return null;
        }

        public async Task<List<EventViewModel>> GetEvents()
        {
            var response = await _http.GetAsync("api/events");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<EventViewModel>>();
            }
            return null;
        }

        public Task<List<EventTypeViewModel>> GetEventTypes()
        {
            throw new NotImplementedException();
        }

        public Task<EventViewModel> UpdateEvent(EventViewModel eventToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
