namespace VTask.Services
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; } = default(T?);
        public bool Success { get; set; } = false;
    }
}
