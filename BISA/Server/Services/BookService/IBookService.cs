namespace BISA.Server.Services.BookService
{
    public interface IBookService
    {
        Task<ServiceResponseDTO<BookDTO>> GetBook(int id);
        Task<ServiceResponseDTO<BookUpdateDTO>> UpdateBook(BookUpdateDTO bookToUpdate);
        Task<ServiceResponseDTO<BookCreateDTO>> CreateBook(BookCreateDTO bookToCreate);
    }
}
