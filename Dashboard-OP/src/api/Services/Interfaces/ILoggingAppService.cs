using Dashboard_OP.src.api.Dtos.LoggingApp;
using Dashboard_OP.src.api.Models;

namespace Dashboard_OP.src.api.Services.Interfaces
{
    public interface ILoggingAppService
    {
        Task<(IReadOnlyList<LogEntry> Items, int TotalCount)> GetAsync(LogQueryDto query, CancellationToken cancellationToken = default);

        Task<LogEntry?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<LogEntry> AddAsync(LogEntry entry, CancellationToken cancellationToken = default);

        Task<LogEntry?> UpdateAsync(int id, UpdateLogEntryDto changes, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
