namespace BISA.Server.Services.BookService
{
    public interface IBookService
    {
        Task<ServiceResponseDTO<BookDTO>> GetBook(int Itemid);
        Task<ServiceResponseDTO<string>> UpdateBook(BookDTO BookToUpdate);
        Task<ServiceResponseDTO<string>> AddBook(BookDTO BookToAdd);
    }
}
