
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
        public async Task<List<ItemDTO>> SearchByTags(string tag)
        {
            List<ItemDTO> itemList = new();

            var items = await _context.Items
                .Where(i => i.Tags.Any(t => t.Tag.ToLower().Contains(tag.ToLower())))
                .Include(i => i.Tags)
                .Include(i => i.ItemInventory).ToListAsync();

            if (items.Count == 0 || items == null)
            {
                throw new NotFoundException("No matching results");
            }

            itemList = ConvertToItemDTO(items);
            return itemList;
            
            
        }

        public async Task<List<ItemDTO>> SearchByTitle(string title)
        {
            List<ItemDTO> itemList = new();

            var items = await _context.Items
                .Include(i => i.ItemInventory)
                .Include(i => i.Tags)                
                .Where(i => i.Title.ToLower().Contains(title.ToLower()))
                .ToListAsync();

            if (items.Count == 0 || items == null)
            {
                throw new NotFoundException("No matching results");
                
            }
            itemList = ConvertToItemDTO(items);
            return itemList;
        }

        public async Task<List<ItemDTO>> SearchByAll(string search)
        {
            List<ItemDTO> itemlist = new();

            var items = await _context.Items
                .Include(i => i.ItemInventory)
                .Include(i => i.Tags)
                .Where(i => i.Title.ToLower().Contains(search.ToLower()) 
                || i.Creator.ToLower().Contains(search.ToLower()) 
                || i.Publisher.ToLower().Contains(search.ToLower()) 
                || i.Date.ToLower().Contains(search.ToLower()) 
                || i.Description.ToLower().Contains(search) 
                || i.Tags.Any(t => t.Tag.ToLower().Contains(search.ToLower())))
                .ToListAsync();

            if (items.Count == 0 || items == null)
            {
                throw new NotFoundException("No matching results");
            }
            itemlist = ConvertToItemDTO(items);
            return itemlist;
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
                    ItemInventory = item.ItemInventory.Count,
                    Description = item.Description,
                    Image = item.Image,
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
