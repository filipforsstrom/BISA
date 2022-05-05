namespace BISA.Server.Services.MovieService
{
    public interface IMovieService
    {
        Task<ServiceResponseDTO<MovieDTO>> GetMovie(int itemId);
        Task<ServiceResponseDTO<string>> UpdateMovie(MovieDTO movieToUpdate);
        Task<ServiceResponseDTO<string>> AddMovie(MovieDTO movieToAdd);
    }
}
