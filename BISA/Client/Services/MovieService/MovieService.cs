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
    }
}
