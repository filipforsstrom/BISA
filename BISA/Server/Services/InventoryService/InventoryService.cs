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

        public async Task<ItemInventoryChangeDTO> AddItemInventory(ItemInventoryChangeDTO itemInventoryAdd)
        {
            var item = await _context.Items
                .Where(i => i.Id == itemInventoryAdd.ItemId)
                .Include(i => i.ItemInventory)
                .FirstOrDefaultAsync();

            if (item == null)
            {
                throw new ArgumentException("Item requested to add inventory to not found");
            }

            for (int i = 0; i < itemInventoryAdd.AmountToAdd; i++)
            {
                item.ItemInventory.Add(new ItemInventoryEntity { ItemId = item.Id, Available = true });
            }

            await _context.SaveChangesAsync();
            return itemInventoryAdd;
        }

        public async Task DeleteItemInventory(int id)
        {
            var inventoryItem = await _context.ItemInventory.Where(i => i.Id == id).FirstOrDefaultAsync();

            if (inventoryItem == null)
            {
                throw new ArgumentException("Inventory item requested for deletion not found.");
            }
            else if (!inventoryItem.Available)
            {
                throw new InvalidOperationException("Inventory item requested for deletion currently loaned out.");
            }

            _context.ItemInventory.Remove(inventoryItem);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ItemInventoryDTO>> GetItemsInventory(int itemId)
        {
            List<ItemInventoryDTO> itemInventoryDTOs = new List<ItemInventoryDTO>();

            var inventory = await _context.ItemInventory.Where(i => i.ItemId == itemId).Include(i => i.Item).ToListAsync();

            foreach(var inventoryItem in inventory)
            {
                itemInventoryDTOs.Add(new ItemInventoryDTO { Id = inventoryItem.Id, ItemId = inventoryItem.ItemId, Available = inventoryItem.Available, Title = inventoryItem.Item.Title }); 
            }

            return itemInventoryDTOs;
        }
    }
}
