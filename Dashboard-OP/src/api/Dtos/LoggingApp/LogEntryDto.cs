using Dashboard_OP.src.api.Models;

namespace Dashboard_OP.src.api.Dtos.LoggingApp
{
    public record LogEntryDto(
        int Id,
        DateTime Timestamp,
        LogSeverity Severity,
        string Application,
        string Message,
        string? Exception,
        string? CorrelationId);
}
