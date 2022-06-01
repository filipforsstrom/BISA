﻿using BISA.Server.Data.DbContexts;
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
              .Any(b => b.Title?.ToLower() == ebookToCreate.Title?.ToLower() &&
              b.Creator?.ToLower() == ebookToCreate.Creator?.ToLower() &&
              b.Date == ebookToCreate.Date &&
              b.Language?.ToLower() == ebookToCreate.Language?.ToLower() &&
              b.Url?.ToLower() == ebookToCreate.Url?.ToLower() &&
              b.Publisher?.ToLower() == ebookToCreate.Publisher?.ToLower());

            if (foundDuplicate)
            {
                responseDTO.Success = false;
                responseDTO.Message = "Ebook already exists.";
                return responseDTO;
            }


            List<TagEntity> tagsForEbook = new List<TagEntity>();

            if (ebookToCreate.Tags.Any())
            {
                foreach (var tag in ebookToCreate.Tags)
                {
                    try
                    {
                        tagsForEbook.Add(_context.Tags.Single(t => t.Id == tag.Id));
                    }
                    catch (Exception)
                    {

                    }
                }
            }

            if (string.IsNullOrEmpty(ebookToCreate.Image))
            {
                ebookToCreate.Image = "/assets/ebook.jpg";
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
                Description = ebookToCreate.Description,
                Image = ebookToCreate.Image,
            };

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

            if (ebook == null)
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

            List<ItemInventoryDTO> ItemInventory = new();
            foreach (var item in ebook.ItemInventory)
            {
                ItemInventory.Add(new ItemInventoryDTO
                { Id = item.Id, ItemId = item.ItemId, Available = item.Available });
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
                ItemInventory = ebook.ItemInventory.Count(),
                Inventory = ItemInventory,
                Description = ebook.Description,
                Image = ebook.Image,
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

            if (ebookToUpdate == null)
            {
                responseDTO.Success = false;
                responseDTO.Message = "Book requested for update not found. ";
                return responseDTO;
            }

            ebookToUpdate.Tags.Clear();

            List<TagEntity> tagsForEbook = new List<TagEntity>();

            if (updatedEbook.Tags.Any())
            {
                foreach (var tag in updatedEbook.Tags)
                {
                    try
                    {
                        tagsForEbook.Add(_context.Tags.Single(m => m.Id == tag.Id));
                    }
                    catch (Exception)
                    {

                    }

                }
            }

            ebookToUpdate.Title = updatedEbook.Title;
            ebookToUpdate.Creator = updatedEbook.Creator;
            ebookToUpdate.Date = updatedEbook.Date;
            ebookToUpdate.Language = updatedEbook.Language;
            ebookToUpdate.Url = updatedEbook.Url;
            ebookToUpdate.Publisher = updatedEbook.Publisher;
            ebookToUpdate.Tags = tagsForEbook;
            ebookToUpdate.Description = updatedEbook.Description;
            ebookToUpdate.Image = updatedEbook.Image;


            _context.Entry(ebookToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            responseDTO.Success = true;
            responseDTO.Data = updatedEbook;
            responseDTO.Message = "Ebook successfully updated";

            return responseDTO;

        }


    }
}
