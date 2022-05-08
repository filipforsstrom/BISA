using BISA.Shared.Entities;

namespace BISA.Server.Services.ItemService
{
    public interface IItemService
    {
        Task<ServiceResponseDTO<ItemDTO>> GetItem(int itemId);
        Task<ServiceResponseDTO<string>> AddItem(string AddItemDTO); // kolla i controller så det stämmer överrens

        Task<ServiceResponseDTO<string>> UpdateItem(string AddItemDTO);
        Task<ServiceResponseDTO<ItemDTO>> DeleteItem(int itemId);
    }
}
