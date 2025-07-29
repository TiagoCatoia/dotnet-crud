namespace PersonApi.Api.Models;

public class ApiError
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = "Erro inesperado";
    public string? Details { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string? TraceId { get; set; }
}