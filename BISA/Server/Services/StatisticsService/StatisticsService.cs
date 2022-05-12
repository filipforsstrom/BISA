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

        public async Task<ServiceResponseDTO<ItemDTO>> GetMostPopularItem()
        {
            ServiceResponseDTO<ItemDTO> responseDTO = new();
            //get complete hisory
            var loanhistory = await _context.LoanHistory.ToListAsync();

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
            
            mostPopularItemDTO.ItemInventory = itemEntity.ItemInventory.Count();

            return mostPopularItemDTO;
        }

        private List<TagDTO> ConvertTagToDTO(List<TagEntity> entityTags)
        {
            List<TagDTO> tagDTOs = new();

            if(entityTags != null)
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
