namespace BISA.Server.Services.BookService
{
    public interface IBookService
    {
        Task<BookDTO> GetBook(int id);
        Task<ServiceResponseDTO<BookUpdateDTO>> UpdateBook(BookUpdateDTO bookToUpdate);
        Task<BookCreateDTO> CreateBook(BookCreateDTO bookToCreate);
    }
}
