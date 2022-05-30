using BISA.Server.Data.DbContexts;
using BISA.Shared.Entities;

namespace BISA.Server.Services.InventoryService
{
    public class InventoryService : IInventoryService
    {
        private readonly BisaDbContext _context;

        public InventoryService(BisaDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponseDTO<ItemInventoryChangeDTO>> AddItemInventory(ItemInventoryChangeDTO itemInventoryAdd)
        {
            ServiceResponseDTO<ItemInventoryChangeDTO> responseDTO = new();

            var item = await _context.Items
                .Where(i => i.Id == itemInventoryAdd.ItemId)
                .Include(i => i.ItemInventory)
                .FirstOrDefaultAsync();

            if (item == null)
            {
                responseDTO.Message = "Item requested to add inventory to not found";
                responseDTO.Success = false;
                return responseDTO;
            }

            for (int i = 0; i < itemInventoryAdd.AmountToAdd; i++)
            {
                item.ItemInventory.Add(new ItemInventoryEntity { ItemId = item.Id, Available = true });
            }

            await _context.SaveChangesAsync();
            responseDTO.Message = $"{itemInventoryAdd.AmountToAdd} inventory items has successfully been added to Item {itemInventoryAdd.ItemId}";
            responseDTO.Success = true;
            return responseDTO;
        }

        public async Task<ServiceResponseDTO<ItemInventoryChangeDTO>> DeleteItemInventory(int id)
        {
            ServiceResponseDTO<ItemInventoryChangeDTO> responseDTO = new();

            var inventoryItem = await _context.ItemInventory.Where(i => i.Id == id).FirstOrDefaultAsync();

            if (inventoryItem == null)
            {
                responseDTO.Message = "Inventory item requested for deletion not found.";
                responseDTO.Success = false;
                return responseDTO;
            }
            else if (!inventoryItem.Available)
            {
                responseDTO.Message = "Inventory item requested for deletion currently loaned out.";
                responseDTO.Success = false;
                return responseDTO;
            }

            _context.ItemInventory.Remove(inventoryItem);

            await _context.SaveChangesAsync();
            responseDTO.Success = true;
            responseDTO.Message = "Inventory Item deleted";
            return responseDTO;
        }

        public async Task<ServiceResponseDTO<List<ItemInventoryDTO>>> GetItemsInventory(int itemId)
        {
            ServiceResponseDTO<List<ItemInventoryDTO>> responseDTO = new();
            List<ItemInventoryDTO> itemInventoryDTOs = new List<ItemInventoryDTO>();

            var inventory = await _context.ItemInventory.Where(i => i.ItemId == itemId).ToListAsync();

            if (!inventory.Any())
            {
                responseDTO.Message = "No inventory found for item.";
                responseDTO.Success = false;
                return responseDTO;
            }

            foreach(var inventoryItem in inventory)
            {
                itemInventoryDTOs.Add(new ItemInventoryDTO { Id = inventoryItem.Id, ItemId = inventoryItem.ItemId, Available = inventoryItem.Available }); 
            }

            responseDTO.Success = true;
            responseDTO.Data = itemInventoryDTOs;
            return responseDTO;
        }
    }
}
