namespace BISA.Server.Services.ItemService
{
    public interface IItemService
    {
        Task<ServiceResponseDTO<ItemDTO>> GetItem(int id);
        Task<ServiceResponseDTO<string>> AddItem(string AddItemDTO); // kolla i controller så det stämmer överrens

        Task<ServiceResponseDTO<string>> UpdateItem(string AddItemDTO);
    }
}
