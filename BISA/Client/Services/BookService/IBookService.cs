using BISA.Shared.DTO;

namespace BISA.Client.Services.BookService
{
    public interface IBookService
    {
        Task<BookViewModel> GetBook(int id);
        Task<string> UpdateBook(BookViewModel bookToUpdate);
        Task<string> CreateBook(BookViewModel bookToCreate);
    }
}
