namespace BISA.Server.Services.MovieService
{
    public interface IMovieService
    {
        Task<ServiceResponseDTO<MovieDTO>> GetMovie(int itemId);
        Task<ServiceResponseDTO<MovieUpdateDTO>> UpdateMovie(MovieUpdateDTO movieToUpdate);
        Task<ServiceResponseDTO<MovieCreateDTO>> CreateMovie(MovieCreateDTO movieToAdd);
    }
}
