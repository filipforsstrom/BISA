
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

        public async Task<ServiceResponseDTO<ItemDTO>> GetItem(int itemId)
        {
            ServiceResponseDTO<ItemDTO> responseDTO = new();

            var itemFromDb = await _context.Items
                .Where(i => i.Id == itemId)
                .Include(i => i.ItemInventory)
                .FirstOrDefaultAsync();

            if (itemFromDb == null)
            {
                responseDTO.Success = false;
                responseDTO.Message = "Item doesn't exist";
                return responseDTO;
            }

            List<ItemInventoryDTO> inventory = new List<ItemInventoryDTO>();
            foreach (var itemInventory in itemFromDb.ItemInventory)
            {
                inventory.Add(new ItemInventoryDTO { Id = itemInventory.Id, ItemId = itemInventory.ItemId, Available = itemInventory.Available });
            }

            var item = new ItemDTO
            {
                Id = itemFromDb.Id,
                Type = itemFromDb.Type,
                Inventory = inventory
            };

            responseDTO.Success = true;
            responseDTO.Data = item;
            return responseDTO;
        }

        public async Task<ServiceResponseDTO<ItemDTO>> DeleteItem(int itemId)
        {
            ServiceResponseDTO<ItemDTO> responseDTO = new();

            var itemToDelete = _context.Items.Where(x => x.Id == itemId)
                .Include(i => i.Tags)
                .Include(i => i.ItemInventory)
                .FirstOrDefault();

            if (itemToDelete == null)
            {
                responseDTO.Success = false;
                responseDTO.Message = "Item requested for deletion not found";
                return responseDTO;
            }

            itemToDelete.ItemInventory.Clear();
            itemToDelete.Tags.Clear();
            _context.Items.Remove(itemToDelete);

            var createdResult = await _context.SaveChangesAsync();

            if(createdResult > 0)
            {
                responseDTO.Success = true;
                responseDTO.Message = "Item deleted";
                return responseDTO;
            }
            
            responseDTO.Success = false;
            responseDTO.Message = "Something went wrong with requested item for deletion";
            return responseDTO;
            
        }


        public async Task<ServiceResponseDTO<List<ItemDTO>>> GetItems()
        {
            ServiceResponseDTO<List<ItemDTO>> responseDTO = new();

            var itemsFromDbList = await _context.Items
                .Include(i => i.Tags)
                .Include(i => i.ItemInventory)
                .ToListAsync();

            if (itemsFromDbList == null)
            {
                responseDTO.Success = false;
                responseDTO.Message = "List of items requested is empty.";
                return responseDTO;
            }

            var listOfItems = new List<ItemDTO>();

            foreach (var item in itemsFromDbList)
            {
                listOfItems.Add(
                    new ItemDTO
                    {
                        Id = item.Id,
                        Title = item.Title,
                        Creator = item.Creator,
                        Date = item.Date,
                        ItemInventory = item.ItemInventory.Count(),
                        Language = item.Language,
                        Publisher = item.Publisher,
                        Type = item.Type,
                        Tags = ConvertTagToTagDTO(item.Tags)
                    });
            }

            responseDTO.Success = true;
            responseDTO.Data = listOfItems;
            return responseDTO;
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

        public async Task<ServiceResponseDTO<List<TagDTO>>> GetTags()
        {
            ServiceResponseDTO<List<TagDTO>> responseDTO = new();
            var tags = await _context.Tags.ToListAsync();

            if(tags == null)
            {
                responseDTO.Success = false;
                responseDTO.Message = "No tags were found";
                return responseDTO;
            }

            var tagDtos = ConvertTagToTagDTO(tags);
            responseDTO.Success = true;
            responseDTO.Data = tagDtos;
            return responseDTO;

        }
    }
}
