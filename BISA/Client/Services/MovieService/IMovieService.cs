namespace BISA.Client.Services.MovieService
{
    public interface IMovieService
    {
        Task<MovieViewModel> GetMovie(int itemId);
        Task<MovieViewModel> UpdateMovie(MovieViewModel movieToUpdate);
        Task<MovieViewModel> CreateMovie(MovieViewModel movieToAdd);
    }
}
