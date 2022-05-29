namespace BISA.Client.Services.MovieService
{
    public interface IMovieService
    {
        Task<MovieViewModel> GetMovie(int itemId);
        Task<ServiceResponseViewModel<string>> UpdateMovie(MovieViewModel movieToUpdate);
        Task<ServiceResponseViewModel<string>> CreateMovie(MovieViewModel movieToAdd);
    }
}
