using BISA.Server.Data.DbContexts;
using BISA.Server.Services.ItemService;
using BISA.Shared.Entities;
using BISA.Shared.ViewModels;
using System.Data;

namespace BISA.Server.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly BisaDbContext _context;


        public BookService(BisaDbContext context)
        {
            _context = context;
        }
        public async Task<BookCreateDTO> CreateBook(BookCreateDTO bookToCreate)
        {
            //See if book already exists.
            var allBooks = await _context.Books.ToListAsync();

            var foundDuplicate = allBooks
                .Any(b => b.Title?.ToLower() == bookToCreate.Title?.ToLower() &&
                b.Creator?.ToLower() == bookToCreate.Creator?.ToLower() &&
                b.Date == bookToCreate.Date &&
                b.Language?.ToLower() == bookToCreate.Language?.ToLower() &&
                b.ISBN?.ToLower() == bookToCreate.ISBN?.ToLower() &&
                b.Publisher?.ToLower() == bookToCreate.Publisher?.ToLower());

            if (foundDuplicate)
            {
                throw new ArgumentException("This book already exists");
            }


            if (string.IsNullOrEmpty(bookToCreate.Image))
            {
                bookToCreate.Image = "/assets/book.jpg";
            }

            List<TagEntity> tagsForMovie = new List<TagEntity>();
            if (bookToCreate.Tags.Any())
            {
                foreach (var tag in bookToCreate.Tags)
                {
                    try
                    {
                        tagsForMovie.Add(_context.Tags.Single(m => m.Id == tag.Id));
                    }
                    catch (Exception)
                    {

                    }

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
                Tags = tagsForMovie,
                Description = bookToCreate.Description,
                Image = bookToCreate.Image,
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

            return bookToCreate;
        }

        public async Task<BookDTO> GetBook(int id)
        {
            //Find books and include the tags & iteminventory-tables to get the books tags & see how many items in inventory.
            var book = _context.Books.Where(b => b.Id == id)
                .Include(b => b.Tags)
                .Include(b => b.ItemInventory)
                .FirstOrDefault();

            if (book == null)
            {
                throw new NotFoundException("There is no book with that id");
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
                Tags = book.Tags.Select(t => new TagDTO { Id = t.Id, Tag = t.Tag }).ToList(),
                ItemInventory = book.ItemInventory.Count(),
                Inventory = book.ItemInventory.Select(it => new ItemInventoryDTO { Id = it.Id, Available = it.Available, ItemId = it.ItemId }).ToList(),
                Description = book.Description,
                Image = book.Image,
            };
            
            return bookDTO;
        }

        public async Task<BookUpdateDTO> UpdateBook(BookUpdateDTO bookToUpdate)
        {
            
            List<TagEntity> tagsForBookToBeUpdated = new();

            var allBooks = await _context.Books.Include(b => b.ItemTags).ToListAsync();

            var foundDuplicate = allBooks
                .Any(b => b.Title?.ToLower() == bookToUpdate.Title?.ToLower() &&
                b.Creator?.ToLower() == bookToUpdate.Creator?.ToLower() &&
                b.Date == bookToUpdate.Date &&
                b.Language?.ToLower() == bookToUpdate.Language?.ToLower() &&
                b.ISBN?.ToLower() == bookToUpdate.ISBN?.ToLower() &&
                b.Publisher?.ToLower() == bookToUpdate.Publisher?.ToLower() &&
                AreTagsEqual(bookToUpdate.Tags, b.ItemTags));

            if (foundDuplicate)
            {
                throw new ArgumentException("A book with these exact properties already exists.");
            }

            var bookEntity = await _context.Books.Include(b => b.Tags).FirstOrDefaultAsync(i => i.Id == bookToUpdate.Id);

            if (bookEntity == null)
            {
                throw new NotFoundException("Book requested for update not found.");
               
            }

            List<TagEntity> tagsForBook = new List<TagEntity>();

            if (bookToUpdate.Tags.Any())
            {
                foreach (var tag in bookToUpdate.Tags)
                {
                    try
                    {
                        tagsForBook.Add(_context.Tags.Single(m => m.Id == tag.Id));
                    }
                    catch (Exception)
                    {



                    }

                }
            }

            bookEntity.Id = bookToUpdate.Id;
            bookEntity.Title = bookToUpdate.Title;
            bookEntity.Creator = bookToUpdate.Creator;
            bookEntity.Date = bookToUpdate.Date;
            bookEntity.Language = bookToUpdate.Language;
            bookEntity.ISBN = bookToUpdate.ISBN;
            bookEntity.Publisher = bookToUpdate.Publisher;
            bookEntity.Tags = tagsForBook;
            bookEntity.Image = bookToUpdate.Image;
            bookEntity.Description = bookToUpdate.Description;


            //Let ef know that the entity we found's state has been modified and then save changes.
            _context.Entry(bookEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return bookToUpdate;
        }

        private bool AreTagsEqual(List<TagViewModel> tags, List<ItemTagEntity> itemTags)
        {
            int numOfEqualTags = 0;

            if(tags.Count != itemTags.Count)
            {
                return false;
            }

            foreach(var tag in itemTags)
            {
                foreach(var tagsViewModel in tags)
                {
                    if(tagsViewModel.Id == tag.TagId)
                    {
                        numOfEqualTags++;
                    }
                }
            }

            if(numOfEqualTags == itemTags.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
