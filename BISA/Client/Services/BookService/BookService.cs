namespace BISA.Client.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly HttpClient _http;

        public BookService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ServiceResponseViewModel<string>> CreateBook(BookViewModel bookToCreate)
        {
            ServiceResponseViewModel<string> serviceResponse = new();
            var response = await _http.PostAsJsonAsync("api/books", bookToCreate);
            if (response.IsSuccessStatusCode)
            {
                serviceResponse.Success = true;
                serviceResponse.Message = "Book created successfully";
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = await response.Content.ReadAsStringAsync();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponseViewModel<BookViewModel>> GetBook(int id)
        {
            ServiceResponseViewModel<BookViewModel> serviceResponse = new();
            var response = await _http.GetAsync($"api/books/{id}");
            if (response.IsSuccessStatusCode)
            {
                serviceResponse.Data = await response.Content.ReadFromJsonAsync<BookViewModel>();
                serviceResponse.Success = true;
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = await response.Content.ReadAsStringAsync();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponseViewModel<string>> UpdateBook(BookViewModel bookToUpdate)
        {
            ServiceResponseViewModel<string> serviceResponse = new();
            var response = await _http.PutAsJsonAsync($"api/books/{bookToUpdate.Id}", bookToUpdate);
            
            if (response.IsSuccessStatusCode)
            {
                serviceResponse.Success = true;
                serviceResponse.Message = "Book successfully updated";
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = await response.Content.ReadAsStringAsync();
            }  
            
            return serviceResponse;
        }
    }
}
