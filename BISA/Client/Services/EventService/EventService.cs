using BISA.Shared.DTO;
using System.Net;

namespace BISA.Client.Services.EventService
{
    public class EventService : IEventService
    {
        private readonly HttpClient _http;

        public EventService(HttpClient http)
        {
            _http = http;
        }
        public async Task<ServiceResponseViewModel<EventViewModel>> CreateEvent(EventViewModel eventToCreate)
        {
            ServiceResponseViewModel<EventViewModel> serviceResponse = new();
            var response = await _http.PostAsJsonAsync($"api/events", eventToCreate);
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                serviceResponse.Data = null;
                serviceResponse.Success = false;
                serviceResponse.Message = await response.Content.ReadAsStringAsync();
            }
            else if (response.IsSuccessStatusCode)
            {
                serviceResponse.Data = await response.Content.ReadFromJsonAsync<EventViewModel>();
                serviceResponse.Success = true;
                serviceResponse.Message = response.ReasonPhrase;
            }
            return serviceResponse;
        }

        public async Task<string> DeleteEvent(int eventId)
        {
            var response = await _http.DeleteAsync($"api/events/{eventId}");
            if (response.IsSuccessStatusCode)
            {
                return $"Event {eventId} successfully deleted";
            }
            else return await response.Content.ReadAsStringAsync();
        }

        public async Task<EventViewModel> GetEvent(int eventId)
        {
            var response = await _http.GetAsync($"api/events/{eventId}");
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

        public async Task<List<EventTypeViewModel>> GetEventTypes()
        {
            var response = await _http.GetAsync("api/events/types");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<EventTypeViewModel>>();
            }
            return null;
        }

        public async Task<EventViewModel> UpdateEvent(EventViewModel eventToUpdate)
        {
            var response = await _http.PutAsJsonAsync($"api/events/{eventToUpdate.Id}", eventToUpdate);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EventViewModel>();
            }
            return null;
        }
    }
}
