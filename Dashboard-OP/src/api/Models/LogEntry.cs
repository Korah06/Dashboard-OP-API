namespace Dashboard_OP.src.api.Models
{
    public class LogEntry
    {
        public int Id { get; set; }

        public DateTime Timestamp { get; set; }

        public LogSeverity Severity { get; set; }

        public string Application { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public string? Exception { get; set; }

        public string? CorrelationId { get; set; }
    }
}
