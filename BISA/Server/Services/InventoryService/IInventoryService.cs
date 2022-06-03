namespace BISA.Server.Services.InventoryService
{
    public interface IInventoryService
    {
        Task<ItemInventoryChangeDTO> AddItemInventory(ItemInventoryChangeDTO itemInventoryAdd);
        Task<List<ItemInventoryDTO>> GetItemsInventory(int itemId);
        Task DeleteItemInventory(int id);

    }
}
