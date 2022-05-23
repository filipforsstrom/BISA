namespace BISA.Client.Services.LoanService
{
    public interface ILoanService
    {
        Task<List<LoanViewModel>> GetAllLoans();
        Task<List<LoanViewModel>> GetMyLoans();
        Task<List<LoanViewModel>> AddLoan(List<ItemViewModel> items);
        Task<string> ReturnLoan(int id);
    }
}
