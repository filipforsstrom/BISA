using BISA.Shared.Entities;

namespace BISA.Server.Services.ItemService
{
    public interface IItemService
    {
        Task<List<ItemDTO>> GetItems();
        Task<ItemDTO> GetItem(int itemId);

        Task<string> DeleteItem(int itemId);
        Task<List<TagDTO>> GetTags();

    }
}
