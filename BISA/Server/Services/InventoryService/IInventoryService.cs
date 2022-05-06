namespace BISA.Server.Services.InventoryService
{
    public interface IInventoryService
    {
        Task <ServiceResponseDTO<List<int>>> GetItemInventory(int id); //kmr vi använda denna? om inte, ta bort
        Task<ServiceResponseDTO<List<int>>> AddItemInventory(int itemId, int amountOfItems);
        Task<ServiceResponseDTO<string>> DeleteItemInventory(int inventoryId);

    }
}
