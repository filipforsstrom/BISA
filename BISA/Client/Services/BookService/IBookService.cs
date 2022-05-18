namespace BISA.Client.Services.BookService
{
    public interface IBookService
    {
        Task<BookViewModel> GetBook(int id);
        //Task<BookViewModel> UpdateBook(BookUpdateDTO bookToUpdate);
        //Task<BookViewModel> CreateBook(BookCreateDTO bookToCreate);
    }
}
