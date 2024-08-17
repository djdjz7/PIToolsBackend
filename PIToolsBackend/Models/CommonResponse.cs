namespace PIToolsBackend.Models
{
    public class CommonResponse<T>
    {
        public T? Data { get; set; }
        public string? Error { get; set; }
        public bool IsSuccess { get; set; }
    }
}
