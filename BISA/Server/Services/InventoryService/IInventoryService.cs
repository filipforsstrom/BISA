namespace BISA.Server.Services.InventoryService
{
    public interface IInventoryService
    {
        Task<ServiceResponseDTO<ItemInventoryDTO>> AddItemInventory(ItemInventoryDTO itemInventoryAdd);
        Task<ServiceResponseDTO<ItemInventoryDTO>> DeleteItemInventory(ItemInventoryDTO itemInventoryDelete);

    }
}
