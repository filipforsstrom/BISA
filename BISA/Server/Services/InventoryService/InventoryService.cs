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

        public async Task<ServiceResponseDTO<ItemInventoryChangeDTO>> DeleteItemInventory(ItemInventoryChangeDTO itemInventoryDelete)
        {
            ServiceResponseDTO<ItemInventoryChangeDTO> responseDTO = new();

            var inventoryItem = await _context.ItemInventory.Where(i => i.Id == itemInventoryDelete.InventoryId).FirstOrDefaultAsync();

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

    }
}
