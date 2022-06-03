
using BISA.Server.Data.DbContexts;
using BISA.Shared.Entities;

namespace BISA.Server.Services.ItemService
{
    public class ItemService : IItemService
    {
        private readonly BisaDbContext _context;

        public ItemService(BisaDbContext context)
        {
            _context = context;
        }

        public async Task<ItemDTO> GetItem(int itemId)
        {

            var itemFromDb = await _context.Items
                .Where(i => i.Id == itemId)
                .Include(i => i.ItemInventory)
                .Include(t => t.Tags)
                .FirstOrDefaultAsync();

            if (itemFromDb == null)
            {
                throw new NotFoundException("Item doesn't exist.");
            }

            var item = new ItemDTO
            {
                Id = itemFromDb.Id,
                Title = itemFromDb.Title,
                Type = itemFromDb.Type,
                Inventory = itemFromDb.ItemInventory.Select(it => new ItemInventoryDTO { Id = it.Id, Available = it.Available, ItemId = it.ItemId}).ToList(),
                Description = itemFromDb.Description,
                Creator = itemFromDb.Creator,
                Tags = itemFromDb.Tags.Select(t => new TagDTO { Id = t.Id, Tag = t.Tag }).ToList()
            };

            return item;
        }

        public async Task<string> DeleteItem(int itemId)
        {

            var itemToDelete = _context.Items.Where(x => x.Id == itemId)
                .Include(i => i.Tags)
                .Include(i => i.ItemInventory)
                .FirstOrDefault();

            if (itemToDelete == null)
            {
                throw new NotFoundException("Item requested for deletion not found");
            }

           if(itemToDelete.ItemInventory.Any(i => i.Available == false))
            {
                throw new InvalidOperationException("Item requested for deletion doesn't have all copies in stock.");
                
            }

            itemToDelete.ItemInventory.Clear();
            itemToDelete.Tags.Clear();
            _context.Items.Remove(itemToDelete);

            var createdResult = await _context.SaveChangesAsync();

            if(createdResult > 0)
            {
                return "Item deleted";
            }
            
            throw new InvalidOperationException("Something went wrong with requested item for deletion");
            
        }


        public async Task<List<ItemDTO>> GetItems()
        {
            var itemsFromDbList = await _context.Items
                .Include(i => i.Tags)
                .Include(i => i.ItemInventory)
                .ToListAsync();

            if (itemsFromDbList == null)
            {
                throw new NotFoundException("List of items requested is empty");
            }

            List<ItemDTO> listOfItems = itemsFromDbList.Select(i => new ItemDTO {
                Id = i.Id,
                Title = i.Title,
                Creator = i.Creator,
                Date = i.Date,
                ItemInventory = i.ItemInventory.Count(),
                Language = i.Language,
                Publisher = i.Publisher,
                Type = i.Type,
                Description = i.Description,
                Image = i.Image,
                Tags = ConvertTagToTagDTO(i.Tags)
            }).ToList();


            return listOfItems;
        }

        private List<TagDTO> ConvertTagToTagDTO(List<TagEntity> tags)
        {
            List<TagDTO> tagsAsDTOs = new();

            foreach (var tag in tags)
            {
                tagsAsDTOs.Add(new TagDTO { Id = tag.Id, Tag = tag.Tag });
            }

            return tagsAsDTOs;
        }

        public async Task<List<TagDTO>> GetTags()
        {
            var tags = await _context.Tags.ToListAsync();

            if(tags == null)
            {
                throw new NotFoundException("No tags were found");
            }

            var tagDtos = ConvertTagToTagDTO(tags);
            return tagDtos;

        }
    }
}
