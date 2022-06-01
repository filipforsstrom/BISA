using BISA.Server.Data.DbContexts;
using BISA.Shared.Entities;

namespace BISA.Server.Services.MovieService
{
    public class MovieService : IMovieService
    {
        private readonly BisaDbContext _context;

        public MovieService(BisaDbContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponseDTO<MovieCreateDTO>> CreateMovie(MovieCreateDTO movieToCreate)
        {
            ServiceResponseDTO<MovieCreateDTO> responseDTO = new();


            var allMovies = await _context.Movies.ToListAsync();

            var foundDuplicate = allMovies
                .Any(m => m.Creator?.ToLower() == movieToCreate.Creator?.ToLower()
                && m.Date == movieToCreate.Date
                && m.Publisher?.ToLower() == movieToCreate.Publisher?.ToLower()
                && m.Language?.ToLower() == movieToCreate.Language?.ToLower()
                && m.Title?.ToLower() == movieToCreate.Title?.ToLower()
                && m.RuntimeInMinutes.Equals(movieToCreate.RuntimeInMinutes));

            if (foundDuplicate)
            {
                responseDTO.Message = "Movie already exists";
                responseDTO.Success = false;
                return responseDTO;
            }

            List<TagEntity> tagsForMovie = new List<TagEntity>();

            if (movieToCreate.Tags.Any())
            {
                foreach (var tag in movieToCreate.Tags)
                {
                    try
                    {
                        tagsForMovie.Add(_context.Tags.Single(m => m.Id == tag.Id));
                    }
                    catch (Exception)
                    {

                    }

                }
            }
            var movieEntity = new MovieEntity
            {

                Title = movieToCreate.Title,
                Language = movieToCreate.Language,
                Date = movieToCreate.Date,
                Publisher = movieToCreate.Publisher,
                Creator = movieToCreate.Creator,
                Tags = tagsForMovie,
                RuntimeInMinutes = movieToCreate.RuntimeInMinutes,
                Description = movieToCreate.Description,
                Image = movieToCreate.Image,

            };

            for (int i = 0; i < movieToCreate.ItemInventory; i++)
            {
                _context.ItemInventory.Add(new ItemInventoryEntity { Item = movieEntity, Available = true });
            }

            _context.Movies.Add(movieEntity);
            await _context.SaveChangesAsync();

            responseDTO.Message = "Movie successfully added";
            responseDTO.Success = true;
            responseDTO.Data = movieToCreate;

            return responseDTO;

        }

        public async Task<ServiceResponseDTO<MovieDTO>> GetMovie(int itemId)
        {
            var response = await _context.Movies.Where(m => m.Id == itemId)
                .Include(m => m.Tags)
                .Include(m => m.ItemInventory)
                .FirstOrDefaultAsync();

            ServiceResponseDTO<MovieDTO> responseDTO = new();

            if (response == null)
            {
                responseDTO.Success = false;
                responseDTO.Message = "Movie not found";
                responseDTO.Data = null;
                return responseDTO;
            }

            List<TagDTO> tags = new();
            foreach (var tag in response.Tags)
            {
                tags.Add(new TagDTO { Id = tag.Id, Tag = tag.Tag });
            }

            List<ItemInventoryDTO> ItemInventory = new();
            foreach (var item in response.ItemInventory)
            {
                ItemInventory.Add(new ItemInventoryDTO
                { Id = item.Id, ItemId = item.ItemId, Available = item.Available });
            }

            responseDTO.Success = true;
            responseDTO.Data = new MovieDTO
            {
                Id = response.Id,
                Title = response.Title,
                Language = response.Language,
                Date = response.Date,
                Publisher = response.Publisher,
                Creator = response.Creator,
                Tags = tags,
                ItemInventory = response.ItemInventory.Count(),
                Inventory = ItemInventory,
                RuntimeInMinutes = response.RuntimeInMinutes,
                Description = response.Description,
                Image = response.Image,
            };
            return responseDTO;

        }

        public async Task<ServiceResponseDTO<MovieUpdateDTO>> UpdateMovie(int id, MovieUpdateDTO updatedMovie)
        {

            var movieToUpdate = await _context.Movies
                .Where(m => m.Id == id)
                .Include(m => m.Tags)
                .FirstOrDefaultAsync();

            ServiceResponseDTO<MovieUpdateDTO> responseDTO = new();

            if (movieToUpdate == null)
            {
                responseDTO.Success = false;
                responseDTO.Message = "Movie requested for update not found.";
                return responseDTO;
            }

            if (movieToUpdate.Tags.Any())
            {
                movieToUpdate.Tags.Clear();
            }

            List<TagEntity> tagsForMovie = new List<TagEntity>();

            if (updatedMovie.Tags.Any())
            {
                foreach (var tag in updatedMovie.Tags)
                {
                    try
                    {
                        tagsForMovie.Add(_context.Tags.Single(m => m.Id == tag.Id));
                    }
                    catch (Exception)
                    {

                    }

                }
            }

            movieToUpdate.Id = updatedMovie.Id;
            movieToUpdate.Title = updatedMovie.Title;
            movieToUpdate.RuntimeInMinutes = updatedMovie.RuntimeInMinutes;
            movieToUpdate.Tags = tagsForMovie;
            movieToUpdate.Language = updatedMovie.Language;
            movieToUpdate.Creator = updatedMovie.Creator;
            movieToUpdate.Date = updatedMovie.Date;
            movieToUpdate.Publisher = updatedMovie.Publisher;
            movieToUpdate.Description = updatedMovie.Description;
            movieToUpdate.Image = updatedMovie.Image;

            _context.Entry(movieToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            responseDTO.Success = true;
            responseDTO.Data = updatedMovie;
            responseDTO.Message = "Movie successfully updated";

            return responseDTO;

        }
    }
}
