namespace BISA.Server.Services.MovieService
{
    public interface IMovieService
    {
        Task<MovieDTO> GetMovie(int itemId);
        Task<MovieUpdateDTO> UpdateMovie(int id, MovieUpdateDTO movieToUpdate);
        Task<MovieCreateDTO> CreateMovie(MovieCreateDTO movieToAdd);
    }
}
