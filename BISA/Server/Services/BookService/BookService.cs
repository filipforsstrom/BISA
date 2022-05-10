using BISA.Server.Data.DbContexts;
using BISA.Server.Services.ItemService;
using BISA.Shared.Entities;

namespace BISA.Server.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly BisaDbContext _context;


        public BookService(BisaDbContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponseDTO<BookCreateDTO>> CreateBook(BookCreateDTO bookToCreate)
        {
            ServiceResponseDTO<BookCreateDTO> responseDTO = new();

            //See if book already exists.
            var allBooks = await _context.Books.ToListAsync();

            var foundDuplicate = allBooks
                .Any(b => b.Title.ToLower() == bookToCreate.Title.ToLower() &&
                b.Creator.ToLower() == bookToCreate.Creator.ToLower() &&
                b.Date.Equals(bookToCreate.Date) &&
                b.Language.ToLower() == bookToCreate.Language.ToLower() &&
                b.ISBN.ToLower() == bookToCreate.ISBN.ToLower() &&
                b.Publisher.ToLower() == b.Publisher.ToLower());

            if (foundDuplicate)
            {
                responseDTO.Success = false;
                responseDTO.Message = "Book already exists.";
                return responseDTO;
            }

            //If bookToCreate gets a list of Tag Ids passed in,
            //search for the tags in the Tags-table and add them to a list.
            List<TagEntity> tagsForBookToBeCreated = new();

            if (bookToCreate.Tags != null && bookToCreate.Tags[0] != 0)
            {
                foreach (var tagIds in bookToCreate.Tags)
                {
                    tagsForBookToBeCreated.Add(_context.Tags.Single(t => t.Id == tagIds));
                }
            }

            //Create book entity for the db.
            var bookEntity = new BookEntity()
            {
                Title = bookToCreate.Title,
                Creator = bookToCreate.Creator,
                Date = bookToCreate.Date,
                Language = bookToCreate.Language,
                ISBN = bookToCreate.ISBN,
                Publisher = bookToCreate.Publisher,
                Tags = tagsForBookToBeCreated,
            };

            //----------------ITEM TO INVENTORY-TABLE LOGIC------------------------//

            //Add how many items there will be of this book to the ItemInventory-table. 
            for (int i = 0; i < bookToCreate.ItemInventory; i++)
            {
                _context.ItemInventory.Add(new ItemInventoryEntity { Item = bookEntity, Available = true });
            }

            //-------------------SAVE ALL AND RETURN DATA-------------------------//

            _context.Books.Add(bookEntity);
            await _context.SaveChangesAsync();
            responseDTO.Success = true;
            responseDTO.Data = bookToCreate;
            responseDTO.Message = "Book successfully created";

            return responseDTO;
        }

        public async Task<ServiceResponseDTO<BookDTO>> GetBook(int Itemid)
        {
            ServiceResponseDTO<BookDTO> responseDTO = new();

            //Find books and include the tags & iteminventory-tables to get the books tags & see how many items in inventory.
            var book = _context.Books.Where(b => b.Id == Itemid)
                .Include(b => b.Tags)
                .Include(b => b.ItemInventory)
                .FirstOrDefault();

            if (book == null)
            {
                responseDTO.Success = false;
                responseDTO.Message = "Book was not found";
                return responseDTO;
            }

            //New list of TagDTOS to assign to the BookDTO.
            //Foreach of the book entities tags, create a new tagDTO and add it to Tags-list.
            List<TagDTO> Tags = new();

            foreach (var tag in book.Tags)
            {
                Tags.Add(new TagDTO { Id = tag.Id, Tag = tag.Tag });
            }

            var bookDTO = new BookDTO()
            {
                Id = book.Id,
                Title = book.Title,
                Creator = book.Creator,
                Date = book.Date,
                Language = book.Language,
                ISBN = book.ISBN,
                Publisher = book.Publisher,
                Tags = Tags,
                ItemInventory = book.ItemInventory.Count()
            };

            responseDTO.Success = true;
            responseDTO.Data = bookDTO;
            return responseDTO;
        }

        public async Task<ServiceResponseDTO<BookUpdateDTO>> UpdateBook(BookUpdateDTO bookToUpdate)
        {
            ServiceResponseDTO<BookUpdateDTO> responseDTO = new();
            List<TagEntity> tagsForBookToBeUpdated = new();

            var bookEntity = await _context.Books.Include(b => b.Tags).FirstOrDefaultAsync(i => i.Id == bookToUpdate.Id);

            if (bookEntity == null)
            {
                responseDTO.Success = false;
                responseDTO.Message = "Book requested for update not found.";
                return responseDTO;
            }

            //Remove current non-updated book's tags.
            bookEntity.Tags.Clear();

            //Add the new tags.
            if (bookToUpdate.Tags != null)
            {
                foreach (var tagIds in bookToUpdate.Tags)
                {
                    tagsForBookToBeUpdated.Add(_context.Tags.Single(t => t.Id == tagIds));
                }
            }


            bookEntity.Id = bookToUpdate.Id;
            bookEntity.Title = bookToUpdate.Title;
            bookEntity.Creator = bookToUpdate.Creator;
            bookEntity.Date = bookToUpdate.Date;
            bookEntity.Language = bookToUpdate.Language;
            bookEntity.ISBN = bookToUpdate.ISBN;
            bookEntity.Publisher = bookToUpdate.Publisher;
            bookEntity.Tags = tagsForBookToBeUpdated;


            //Let ef know that the entity we found's state has been modified and then save changes.
            _context.Entry(bookEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            responseDTO.Success = true;
            responseDTO.Data = bookToUpdate;
            responseDTO.Message = "Book successfully updated";

            return responseDTO;
        }

        //public async Task<List<TagEntity>> GetTags(List<TagDTO> tagsDtos)
        //{
        //    List<TagEntity> tagsToBook = new List<TagEntity>();

        //    var Tags = await _context.Tags.ToListAsync();

        //    foreach (var tagId in tagsDtos)
        //    {
        //        foreach (var tag in Tags)
        //        {
        //            if (tag.Id == tagId.Id)
        //            {
        //                tagsToBook.Add(tag);
        //            }
        //        }
        //    }


        //    return tagsToBook;
        //}
    }
}
