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
        public async Task<MovieCreateDTO> CreateMovie(MovieCreateDTO movieToCreate)
        {

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
                throw new ArgumentException(" This movie already exists");
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

            if (string.IsNullOrEmpty(movieToCreate.Image))
            {
                movieToCreate.Image = "/assets/movie.jpg";
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

            return movieToCreate;

            

        }

        public async Task<MovieDTO> GetMovie(int itemId)
        {
            var movie = await _context.Movies.Where(m => m.Id == itemId)
                .Include(m => m.Tags)
                .Include(m => m.ItemInventory)
                .FirstOrDefaultAsync();

            ServiceResponseDTO<MovieDTO> responseDTO = new();

            if (movie == null)
            {
                throw new NotFoundException("Movie not found");
            }

     


            var movieDto = new MovieDTO()
            {
                Id = movie.Id,
                Title = movie.Title,
                Language = movie.Language,
                Date = movie.Date,
                Publisher = movie.Publisher,
                Creator = movie.Creator,
                Tags = movie.Tags.Select(t => new TagDTO { Id = t.Id, Tag = t.Tag }).ToList(),
                ItemInventory = movie.ItemInventory.Count(),
                Inventory = movie.ItemInventory.Select(it => new ItemInventoryDTO { Id = it.Id, Available = it.Available, ItemId = it.ItemId }).ToList(),
                RuntimeInMinutes = movie.RuntimeInMinutes,
                Description = movie.Description,
                Image = movie.Image,
            };
            return movieDto;

        }

        public async Task<MovieUpdateDTO> UpdateMovie(int id, MovieUpdateDTO updatedMovie)
        {
            var allmovies = await _context.Movies.ToListAsync();

            var foundDuplicate = allmovies
                .Any(b => b.Title?.ToLower() == updatedMovie.Title?.ToLower() &&
                b.Creator?.ToLower() == updatedMovie.Creator?.ToLower() &&
                b.Date == updatedMovie.Date &&
                b.Language?.ToLower() == updatedMovie.Language?.ToLower() &&
                b.RuntimeInMinutes == updatedMovie.RuntimeInMinutes &&
                b.Publisher?.ToLower() == updatedMovie.Publisher?.ToLower());

            if (foundDuplicate)
            {
                throw new ArgumentException("A movie with these exact properties already exists.");
            }


            var movieToUpdate = await _context.Movies
                .Where(m => m.Id == id)
                .Include(m => m.Tags)
                .FirstOrDefaultAsync();

            ServiceResponseDTO<MovieUpdateDTO> responseDTO = new();

            if (movieToUpdate == null)
            {
                throw new NotFoundException("Movie requested for update not found.");
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

            return updatedMovie;

        }
    }
}
