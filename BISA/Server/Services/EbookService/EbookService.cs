using BISA.Server.Data.DbContexts;
using BISA.Shared.Entities;

namespace BISA.Server.Services.EbookService
{
    public class EbookService : IEbookService
    {
        private readonly BisaDbContext _context;

        public EbookService(BisaDbContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponseDTO<EbookCreateDTO>> CreateEbook(EbookCreateDTO ebookToCreate)
        {
            ServiceResponseDTO<EbookCreateDTO> responseDTO = new();
            

            var allEbooks = await _context.Ebooks.ToListAsync();

            var foundDuplicate = allEbooks
              .Any(b => b.Title.ToLower() == ebookToCreate.Title.ToLower() &&
              b.Creator.ToLower() == ebookToCreate.Creator.ToLower() &&
              b.Date.Equals(ebookToCreate.Date) &&
              b.Language.ToLower() == ebookToCreate.Language.ToLower() &&
              b.Url.ToLower() == ebookToCreate.Url.ToLower() &&
              b.Publisher.ToLower() == ebookToCreate.Publisher.ToLower());

            if (foundDuplicate)
            {
                responseDTO.Success = false;
                responseDTO.Message = "Book already exists.";
                return responseDTO;
            }

            
            List<TagEntity> tagsForEbook = new List<TagEntity>();

            if (ebookToCreate.Tags != null) 
            { 
                foreach (var tagId in ebookToCreate.Tags)
                {
                    try
                    {
                        tagsForEbook.Add(_context.Tags.Single(t => t.Id == tagId));
                    }
                    catch (Exception)
                    {

                    }  
                }
            }

            var ebookEntity = new EbookEntity
            {
                Title = ebookToCreate.Title,
                Creator = ebookToCreate.Creator,
                Date = ebookToCreate.Date,
                Language = ebookToCreate.Language,
                Url = ebookToCreate.Url,
                Publisher = ebookToCreate.Publisher,
                Tags = tagsForEbook,
            };

            //adding nr of copies of ebook to inventory
            for (int i = 0; i < ebookToCreate.ItemInventory; i++)
            {
                _context.ItemInventory.Add(new ItemInventoryEntity { Item = ebookEntity, Available = true });
            }

            _context.Ebooks.Add(ebookEntity);
            await _context.SaveChangesAsync();

            responseDTO.Success = true;
            responseDTO.Message = "Ebook successfully created";
            responseDTO.Data = ebookToCreate;

            return responseDTO;

        }

        public async Task<ServiceResponseDTO<EbookDTO>> GetEbook(int itemId)
        {
            ServiceResponseDTO<EbookDTO> responseDTO = new();

            var ebook = _context.Ebooks
                .Where(e => e.Id == itemId)
                .Include(e => e.Tags)
                .Include(e => e.ItemInventory)
                .FirstOrDefault();

            if(ebook == null)
            {
                responseDTO.Success = false;
                responseDTO.Message = "Ebook not found";
                return responseDTO;
            }


            List<TagDTO> tags = new();

            foreach (var tag in ebook.Tags)
            {
                tags.Add(new TagDTO { Id = tag.Id, Tag = tag.Tag });
            }

            var ebookDTO = new EbookDTO()
            {
                Id = ebook.Id,
                Title = ebook.Title,
                Creator = ebook.Creator,
                Date = ebook.Date,
                Language = ebook.Language,
                Url = ebook.Url,
                Publisher = ebook.Publisher,
                Tags = tags,
                ItemInventory = ebook.ItemInventory.Count()
            };

            responseDTO.Success = true;
            responseDTO.Data = ebookDTO;
            return responseDTO;

        }

        public async Task<ServiceResponseDTO<EbookUpdateDTO>> UpdateEbook(EbookUpdateDTO updatedEbook)
        {
            ServiceResponseDTO<EbookUpdateDTO> responseDTO = new();

            var ebookToUpdate = await _context.Ebooks.Where(e => e.Id == updatedEbook.Id)
                .Include(e => e.Tags)
                .FirstOrDefaultAsync();

            if(ebookToUpdate == null)
            {
                responseDTO.Success = false;
                responseDTO.Message = "Book requested for update not found. ";
                return responseDTO;
            }

            ebookToUpdate.Tags.Clear();

            List<TagEntity> tagsForEbook= new List<TagEntity>();

            if (updatedEbook.Tags != null)
            {
                foreach (var tagId in updatedEbook.Tags)
                {
                    try
                    {
                        tagsForEbook.Add(_context.Tags.Single(m => m.Id == tagId));
                    }
                    catch (Exception)
                    {

                    }
                    
                }
            }

            //ebookToUpdate.Id = updatedEbook.Id;
            ebookToUpdate.Title = updatedEbook.Title;
            ebookToUpdate.Creator = updatedEbook.Creator;
            ebookToUpdate.Date = updatedEbook.Date;
            ebookToUpdate.Language = updatedEbook.Language;
            ebookToUpdate.Url = updatedEbook.Url;
            ebookToUpdate.Publisher = updatedEbook.Publisher;
            ebookToUpdate.Tags = tagsForEbook;


            _context.Entry(ebookToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            responseDTO.Success = true;
            responseDTO.Data = updatedEbook;
            responseDTO.Message = "Ebook successfully updated";

            return responseDTO;

        }

        
    }
}
