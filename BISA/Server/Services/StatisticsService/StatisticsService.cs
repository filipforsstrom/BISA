using BISA.Server.Data.DbContexts;
using BISA.Shared.Entities;

namespace BISA.Server.Services.StatisticsService
{
    public class StatisticsService : IStatisticsService
    {
        private readonly BisaDbContext _context;

        public StatisticsService(BisaDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponseDTO<UserStatisticsDTO>> GetMostActiveUser()
        {
           ServiceResponseDTO<UserStatisticsDTO> serviceResponseDTO = new();
           UserStatisticsDTO mostPopularUserStatisticsDTO = new();

            //get complete loan history
            var loanhistory = await _context.LoansHistory.ToListAsync();

            //grouping list of most popular user
            var sorterUsers = loanhistory.GroupBy(l => l.UserId).OrderByDescending(g => g.Count()).Select(g => g).First();

            //number of users loans added to DTO
            mostPopularUserStatisticsDTO.NumberOfLoans = sorterUsers.Count();

            //getting the most popular user id from grouping key
            var mostPopularUserId = sorterUsers.Key;

            // getting user from db
            var user = await _context.Users.Where(u => u.Id == mostPopularUserId).FirstOrDefaultAsync();

            //assigning users email to DTO 
            mostPopularUserStatisticsDTO.Email = user.Email;

            
            serviceResponseDTO.Data = mostPopularUserStatisticsDTO;
            serviceResponseDTO.Success = true;

            return serviceResponseDTO;
        }

        public async Task<ServiceResponseDTO<MostPopularAuthorDTO>> GetMostPopularAuthor()
        {
            ServiceResponseDTO<MostPopularAuthorDTO> serviceResponseDTO = new();
            MostPopularAuthorDTO mostPopularAuthor = new();

            //get complete loan history
            var loanhistory = await _context.LoansHistory.Select(i => i.ItemInventoryId).ToListAsync();

            List<ItemInventoryEntity> allItemsInHistory = new();

            foreach (var id in loanhistory)
            {
                var actualItem = _context.ItemInventory.Include(i => i.Item).Where(i => i.Id == id).First();
                allItemsInHistory.Add(actualItem);
            }

            var sortedItems = allItemsInHistory.GroupBy(l => l.Item.Creator).OrderByDescending(l => l.Count()).Select(l => l).First();

            mostPopularAuthor.NumberOfLoans = sortedItems.Count();
            mostPopularAuthor.Name = sortedItems.Key;

            serviceResponseDTO.Data = mostPopularAuthor;
            serviceResponseDTO.Success = true;

            return serviceResponseDTO;
        }

        public async Task<ServiceResponseDTO<ItemDTO>> GetMostPopularItem()
        {
            ServiceResponseDTO<ItemDTO> responseDTO = new();
            //get complete loan history
            var loanhistory = await _context.LoansHistory.ToListAsync();

            //grouping list of most popular items
            var sortedLoans = loanhistory.GroupBy(l => l.ItemInventoryId).OrderByDescending(g => g.Count()).Select(g => g).First();

            //getting the most popular inventory id from grouping key
            int mostPopularInventoryId = sortedLoans.Key;

            //getting most popular inventoryItem entity
            var popInventoryItem = await _context.ItemInventory.Where(i => i.Id == mostPopularInventoryId).Include(i => i.Item).FirstOrDefaultAsync();

            //creating most popular itemEntitiy
            var itemEntity = popInventoryItem.Item;

            var mostPopularItemDTO = ConvertEntityToDTO(itemEntity);

            responseDTO.Success = true;
            responseDTO.Data = mostPopularItemDTO;

            return responseDTO;


        }

        private ItemDTO ConvertEntityToDTO(ItemEntity itemEntity)
        {
            ItemDTO mostPopularItemDTO = new();

            mostPopularItemDTO.Id = itemEntity.Id;
            mostPopularItemDTO.Title = itemEntity.Title;
            mostPopularItemDTO.Publisher = itemEntity.Publisher;
            mostPopularItemDTO.Creator = itemEntity.Creator;
            mostPopularItemDTO.Language = itemEntity.Language;
            mostPopularItemDTO.Date = itemEntity.Date;
            mostPopularItemDTO.Tags = ConvertTagToDTO(itemEntity.Tags);
            mostPopularItemDTO.Type = itemEntity.Type.ToLower();

            //mostPopularItemDTO.ItemInventory = itemEntity.ItemInventory.Count();

            return mostPopularItemDTO;
        }

        private List<TagDTO> ConvertTagToDTO(List<TagEntity> entityTags)
        {
            List<TagDTO> tagDTOs = new();

            if (entityTags != null)
            {
                foreach (var tag in entityTags)
                {
                    try
                    {
                        tagDTOs.Add(new TagDTO { Id = tag.Id, Tag = tag.Tag });
                    }
                    catch (Exception)
                    {
                    }

                }
            }


            return tagDTOs;
        }
    }
}
