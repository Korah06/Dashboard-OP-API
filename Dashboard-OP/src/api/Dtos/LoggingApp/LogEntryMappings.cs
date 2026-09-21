using Dashboard_OP.src.api.Models;

namespace Dashboard_OP.src.api.Dtos.LoggingApp
{
    public static class LogEntryMappings
    {
        public static LogEntryDto ToDto(this LogEntry entry) => new(
            entry.Id,
            entry.Timestamp,
            entry.Severity,
            entry.Application,
            entry.Message,
            entry.Exception,
            entry.CorrelationId);

        public static LogEntry ToModel(this CreateLogEntryDto dto) => new()
        {
            Timestamp = DateTime.UtcNow,
            Severity = dto.Severity,
            Application = dto.Application,
            Message = dto.Message,
            Exception = dto.Exception,
            CorrelationId = dto.CorrelationId
        };
    }
}
