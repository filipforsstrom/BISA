namespace BISA.Client.Services.LoanService
{
    public class LoanService : ILoanService
    {
        private readonly HttpClient _httpClient;

        public LoanService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<LoanViewModel>> GetAllLoans()
        {
            throw new NotImplementedException();
        }

        public async Task<List<LoanViewModel>> GetMyLoans()
        {
            var httpResponse = await _httpClient.GetAsync("api/loans/user");
            if (httpResponse.IsSuccessStatusCode)
            {
                var loanList = await httpResponse.Content.ReadFromJsonAsync<List<LoanViewModel>>();
                return loanList;
            }

            return null;
        }

        public async Task<List<LoanViewModel>> AddLoan(List<ItemViewModel> items)
        {
            throw new NotImplementedException();
        }

        public async Task<string> ReturnLoan(int id)
        {
            throw new NotImplementedException();
        }
    }
}
