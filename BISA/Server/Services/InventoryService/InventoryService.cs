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

        public async Task<ServiceResponseDTO<List<int>>> AddItemInventory(ItemInventoryDTO itemInventoryAdd)
        {
            ServiceResponseDTO<List<int>> responseDTO = new();

            var item = await _context.Items
                .Where(i => i.Id == itemInventoryAdd.ItemId)
                .Include(i => i.ItemInventory).FirstOrDefaultAsync();

            if(item == null)
            {
                responseDTO.Message = "Item requested to add inventory to not found";
                responseDTO.Success = false;
                return responseDTO;
            }

            for (int i = 0; i < itemInventoryAdd.AmountOfItems; i++)
            {
                item.ItemInventory.Add(new ItemInventoryEntity { ItemId = item.Id, Available = true });
            }

            await _context.SaveChangesAsync();
            responseDTO.Message = "Items successfully added";
            responseDTO.Success = true;
            return responseDTO;
        }

        public async Task<ServiceResponseDTO<ItemInventoryDTO>> DeleteItemInventory(ItemInventoryDTO itemInventoryDelete)
        {
            ServiceResponseDTO<ItemInventoryDTO> responseDTO = new();

            var inventoryItem = await _context.ItemInventory.Where(i => i.Id == itemInventoryDelete.InventoryId).FirstOrDefaultAsync();

            if (inventoryItem == null || !inventoryItem.Available)
            {
                responseDTO.Message = "Inventory item requested for deletion either not found or currently loaned out.";
                responseDTO.Success = false;
                return responseDTO;
            }

            _context.ItemInventory.Remove(inventoryItem);

            await _context.SaveChangesAsync();
            responseDTO.Success=true;
            responseDTO.Message = "Inventory Item deleted";
            return responseDTO;
        }

        public async Task<ServiceResponseDTO<List<int>>> GetItemInventory(ItemInventoryDTO itemInventory)
        {
            throw new NotImplementedException();
        }
    }
}
