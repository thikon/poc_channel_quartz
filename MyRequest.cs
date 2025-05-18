public class MyRequest
{
    public string Payload { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}