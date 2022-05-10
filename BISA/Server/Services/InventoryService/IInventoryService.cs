namespace BISA.Server.Services.InventoryService
{
    public interface IInventoryService
    {
        Task <ServiceResponseDTO<List<int>>> GetItemInventory(ItemInventoryDTO itemInventory); //kmr vi använda denna? om inte, ta bort
        Task<ServiceResponseDTO<List<int>>> AddItemInventory(ItemInventoryDTO itemInventoryAdd);
        Task<ServiceResponseDTO<ItemInventoryDTO>> DeleteItemInventory(ItemInventoryDTO itemInventoryDelete);

    }
}
