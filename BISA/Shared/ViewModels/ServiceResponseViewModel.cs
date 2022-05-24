namespace BISA.Shared.ViewModels
{
    public class ServiceResponseViewModel<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
