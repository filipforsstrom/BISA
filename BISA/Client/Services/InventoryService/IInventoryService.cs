namespace BISA.Client.Services.InventoryService
{
    public interface IInventoryService
    {
        Task<ServiceResponseViewModel<List<ItemInventoryViewModel>>> GetItemInventory(int itemId);
        Task<ServiceResponseViewModel<string>> DeleteItemInventory(int id);
    }
}
