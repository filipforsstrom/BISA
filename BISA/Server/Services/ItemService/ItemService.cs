using BISA.Server.Data.DbContexts;
using BISA.Shared.Entities;

namespace BISA.Server.Services.ItemService
{
    public class ItemService : IItemService
    {
        private readonly BisaDbContext _context;

        public ItemService(BisaDbContext context)
        {
            _context = context;
        }

        public Task<ServiceResponseDTO<string>> AddItem(string AddItemDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponseDTO<ItemDTO>> DeleteItem(int itemId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponseDTO<ItemDTO>> GetItem(int id)
        {
            //var item = _context.Items.Where(i => i.Id == id && i.Type == "Book").FirstOrDefault();
            throw new NotImplementedException();

        }

        public Task<ServiceResponseDTO<string>> UpdateItem(string AddItemDTO)
        {
            throw new NotImplementedException();
        }
    }
}
