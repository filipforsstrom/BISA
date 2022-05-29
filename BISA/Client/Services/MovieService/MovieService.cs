﻿using BISA.Shared.DTO;

namespace BISA.Client.Services.MovieService
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient _http;

        public MovieService(HttpClient http)
        {
            _http = http;
        }

        public async Task<MovieViewModel> GetMovie(int itemId)
        {
            var response = await _http.GetAsync($"api/movies/{itemId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<MovieViewModel>();
            }
            else return null;
            
        }
        public async Task<ServiceResponseViewModel<string>> CreateMovie(MovieViewModel movieToCreate)
        {
            ServiceResponseViewModel<string> serviceResponse = new();
            var response = await _http.PostAsJsonAsync("api/movies", movieToCreate);
            if(response.IsSuccessStatusCode)
            {
                serviceResponse.Success = true;
                serviceResponse.Message = await response.Content.ReadAsStringAsync();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = await response.Content.ReadAsStringAsync();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponseViewModel<string>> UpdateMovie(MovieViewModel movieToUpdate)
        {
            ServiceResponseViewModel<string> serviceResponse = new();
            var response = await _http.PutAsJsonAsync($"api/movies/{movieToUpdate.Id}", movieToUpdate);

            if (response.IsSuccessStatusCode)
            {
                serviceResponse.Success = true;
                serviceResponse.Message = await response.Content.ReadAsStringAsync();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = await response.Content.ReadAsStringAsync();
            }

            return serviceResponse;
        }
    }
}
