using BISA.Shared.Entities;

namespace BISA.Server.Services.ItemService
{
    public interface IItemService
    {
        Task<ServiceResponseDTO<List<ItemDTO>>> GetItems();
        Task<ServiceResponseDTO<ItemDTO>> GetItem(int itemId);

        Task<ServiceResponseDTO<ItemDTO>> DeleteItem(int itemId);
        Task<ServiceResponseDTO<List<TagDTO>>> GetTags();

    }
}
