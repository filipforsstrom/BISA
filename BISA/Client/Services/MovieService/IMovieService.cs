namespace BISA.Client.Services.MovieService
{
    public interface IMovieService
    {
        Task<MovieViewModel> GetMovie(int itemId);
        //Task<MovieViewModel> UpdateMovie(MovieUpdateDTO movieToUpdate);
        //Task<MovieViewModel> CreateMovie(MovieCreateDTO movieToAdd);
    }
}
