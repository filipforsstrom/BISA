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

        public async Task<ServiceResponseViewModel<string>> DeleteEvent(int eventId)
        {
            ServiceResponseViewModel<string> serviceResponse = new();
            var response = await _http.DeleteAsync($"api/events/{eventId}");
            if (response.IsSuccessStatusCode)
            {
                serviceResponse.Success = true;
                serviceResponse.Message = $"Event {eventId} successfully deleted";
                return serviceResponse;
            }

            serviceResponse.Success = false;
            serviceResponse.Message = await response.Content.ReadAsStringAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponseViewModel<EventViewModel>> GetEvent(int eventId)
        {
            ServiceResponseViewModel<EventViewModel> serviceResponse = new();
            var response = await _http.GetAsync($"api/events/{eventId}");
            if (response.IsSuccessStatusCode)
            {
                serviceResponse.Success = true;
                serviceResponse.Data = await response.Content.ReadFromJsonAsync<EventViewModel>();
                return serviceResponse;
            }

            serviceResponse.Success = false;
            serviceResponse.Message = await response.Content.ReadAsStringAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponseViewModel<List<EventViewModel>>> GetEvents()
        {
            ServiceResponseViewModel<List<EventViewModel>> serviceResponse = new();
            var response = await _http.GetAsync("api/events");
            if (response.IsSuccessStatusCode)
            {
                serviceResponse.Success = true;
                serviceResponse.Data = await response.Content.ReadFromJsonAsync<List<EventViewModel>>();
                return serviceResponse;
            }

            serviceResponse.Success = false;
            serviceResponse.Message = await response.Content.ReadAsStringAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponseViewModel<List<EventTypeViewModel>>> GetEventTypes()
        {
            ServiceResponseViewModel<List<EventTypeViewModel>> serviceResponse = new();
            var response = await _http.GetAsync("api/events/types");
            if (response.IsSuccessStatusCode)
            {
                serviceResponse.Success = true;
                serviceResponse.Data = await response.Content.ReadFromJsonAsync<List<EventTypeViewModel>>();
                return serviceResponse;
            }

            serviceResponse.Success = false;
            serviceResponse.Message = await response.Content.ReadAsStringAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponseViewModel<EventViewModel>> UpdateEvent(EventViewModel eventToUpdate)
        {
            ServiceResponseViewModel<EventViewModel> serviceResponse = new();

            var response = await _http.PutAsJsonAsync($"api/events/{eventToUpdate.Id}", eventToUpdate);
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
    }
}
