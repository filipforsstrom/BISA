namespace BISA.Server.Services.InventoryService
{
    public interface IInventoryService
    {
        Task<ServiceResponseDTO<ItemInventoryChangeDTO>> AddItemInventory(ItemInventoryChangeDTO itemInventoryAdd);
        Task<ServiceResponseDTO<ItemInventoryChangeDTO>> DeleteItemInventory(ItemInventoryChangeDTO itemInventoryDelete);

    }
}
