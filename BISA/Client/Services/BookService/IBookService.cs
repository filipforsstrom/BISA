using BISA.Shared.DTO;

namespace BISA.Client.Services.BookService
{
    public interface IBookService
    {
        Task<ServiceResponseViewModel<BookViewModel>> GetBook(int id);
        Task<ServiceResponseViewModel<string>> UpdateBook(BookViewModel bookToUpdate);
        Task<ServiceResponseViewModel<string>> CreateBook(BookViewModel bookToCreate);
    }
}
