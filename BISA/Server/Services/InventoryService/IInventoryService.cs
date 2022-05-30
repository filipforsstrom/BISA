namespace BISA.Server.Services.InventoryService
{
    public interface IInventoryService
    {
        Task<ServiceResponseDTO<ItemInventoryChangeDTO>> AddItemInventory(ItemInventoryChangeDTO itemInventoryAdd);
        Task<ServiceResponseDTO<List<ItemInventoryDTO>>> GetItemsInventory(int itemId);
        Task<ServiceResponseDTO<ItemInventoryChangeDTO>> DeleteItemInventory(int id);

    }
}
