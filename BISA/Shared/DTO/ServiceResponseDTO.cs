namespace BISA.Shared.DTO
{
    public class ServiceResponseDTO<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public static implicit operator ServiceResponseDTO<T>(ServiceResponseDTO<List<EventDTO>> v)
        {
            throw new NotImplementedException();
        }
    }
}
