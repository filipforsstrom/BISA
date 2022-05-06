namespace BISA.Server.Services.InventoryService
{
    public interface IInventoryService
    {
        Task <ServiceResponseDTO<List<int>>> GetInventory (); //kmr vi använda denna? om inte, ta bort
        Task<ServiceResponseDTO<List<int>>> AddInventory(int itemId, int amountOfItems);
        Task<ServiceResponseDTO<string>> DeleteInventory (int inventoryId);

    }
}
