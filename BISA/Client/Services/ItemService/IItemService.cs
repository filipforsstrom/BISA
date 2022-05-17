namespace BISA.Client.Services.ItemService
{
    public interface IItemService
    {
        Task<List<ItemViewModel>> GetItems();
        Task<ItemViewModel> GetItem(int id);
        Task<ItemViewModel> DeleteItem(int id);
    }
}
