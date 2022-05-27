namespace BISA.Client.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly HttpClient _http;

        public BookService(HttpClient http)
        {
            _http = http;
        }

        public async Task<string> CreateBook(BookViewModel bookToCreate)
        {
            var response = await _http.PostAsJsonAsync("api/books", bookToCreate);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<BookViewModel> GetBook(int id)
        {
            var response = await _http.GetAsync($"api/books/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<BookViewModel>();
            }
            else return null;
        }

        public Task<BookViewModel> UpdateBook(BookViewModel bookToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
