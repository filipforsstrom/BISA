
using BISA.Server.Data.DbContexts;
using BISA.Shared.DTO;
using BISA.Shared.Entities;

namespace BISA.Server.Services.SearchService
{
    public class SearchService : ISearchService
    {
        private readonly BisaDbContext _context;

        public SearchService(BisaDbContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponseDTO<List<ItemDTO>>> SearchByTags(string tag)
        {
            ServiceResponseDTO<List<ItemDTO>> response = new();

            var items = await _context.Items
                .Include(i => i.Tags)
                .Include(i => i.ItemInventory)
                .Where(i => i.Tags.Select(t => t.Tag).Contains(tag))
                .ToListAsync();

            if (items != null)
            {
                response.Data = ConvertToItemDTO(items);
                response.Success = true;
                return response;
            }
            response.Success = false;
            response.Message = "No matching results";
            return response;
        }

        public async Task<ServiceResponseDTO<List<ItemDTO>>> SearchByTitle(string title)
        {
            ServiceResponseDTO<List<ItemDTO>> response = new();

            var items = await _context.Items
                .Include(i => i.ItemInventory)
                .Include(i => i.Tags)                
                .Where(i => i.Title.ToLower().Contains(title.ToLower()))
                .ToListAsync();

            if (items != null)
            {
                response.Data = ConvertToItemDTO(items);
                response.Success = true;
                return response;
            }
            response.Success = false;
            response.Message = "No matching results";
            return response;
        }

        public async Task<ServiceResponseDTO<List<ItemDTO>>> SearchByAll(string search)
        {
            ServiceResponseDTO<List<ItemDTO>> response = new();

            var items = await _context.Items
                .Include(i => i.ItemInventory)
                .Include(i => i.Tags)
                .Where(i => i.Title.ToLower().Contains(search.ToLower()) ||
                i.Creator.ToLower().Contains(search.ToLower()) ||
                i.Publisher.ToLower().Contains(search.ToLower()) ||
                i.Date.ToLower().Contains(search.ToLower()) || i.Tags.Any(t => t.Tag.ToLower().Contains(search.ToLower())))
                .ToListAsync();

            if (items != null)
            {
                response.Data = ConvertToItemDTO(items);
                response.Success = true;
                return response;
            }

            response.Success = false;
            response.Message = "No matching results";
            return response;
        }

        private List<ItemDTO> ConvertToItemDTO(List<ItemEntity> itemsInDb)
        {
            List<ItemDTO> result = new();

            foreach (var item in itemsInDb)
            {
                result.Add(new ItemDTO
                {
                    Id = item.Id,
                    Title = item.Title,
                    Language = item.Language,
                    Date = item.Date,
                    Publisher = item.Publisher,
                    Creator = item.Creator,
                    Type = item.Type,
                    Tags = ConvertTagsToTagDTOs(item.Tags),
                    ItemInventory = item.ItemInventory.Count
                });
            }

            return result;
        }

        private List<TagDTO> ConvertTagsToTagDTOs(List<TagEntity> tags)
        {
            List<TagDTO> tagsAsDTOs = new();

            foreach(var tag in tags)
            {
                tagsAsDTOs.Add(new TagDTO { Id = tag.Id, Tag = tag.Tag });
            }

            return tagsAsDTOs;
        }
    }
}
