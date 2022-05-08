namespace BISA.Server.Services.BookService
{
    public interface IBookService
    {
        Task<ServiceResponseDTO<BookDTO>> GetBook(int Itemid);
        Task<ServiceResponseDTO<BookDTO>> UpdateBook(BookDTO bookToUpdate);
        Task<ServiceResponseDTO<BookCreateDTO>> CreateBook(BookCreateDTO bookToCreate);
    }
}
