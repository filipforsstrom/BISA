namespace BISA.Server.Services.LoanService
{
    public interface ILoanService
    {
        Task<ServiceResponseDTO<List<string>>> GetAllLoans();
        Task<ServiceResponseDTO<List<string>>> GetMyLoans(int id);
        Task<ServiceResponseDTO<List<string>>> AddLoan();
        Task<ServiceResponseDTO<string>> ReturnLoan();
    }
}
