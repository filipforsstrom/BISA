namespace BISA.Client.Services.MovieService
{
    public interface IMovieService
    {
        Task<MovieViewModel> GetMovie(int itemId);
        Task<string> UpdateMovie(MovieViewModel movieToUpdate);
        Task<string> CreateMovie(MovieViewModel movieToAdd);
    }
}
