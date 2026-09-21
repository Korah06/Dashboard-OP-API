using Dashboard_OP.src.api.Dtos.LoggingApp;
using Dashboard_OP.src.api.Models;
using Dashboard_OP.src.api.Services.Interfaces;

namespace Dashboard_OP.src.api.Services.Implementations
{
    /// <summary>
    /// In-memory mock of <see cref="ILoggingAppService"/> seeded with example data.
    /// Register as a singleton so state survives across requests.
    /// </summary>
    public class MockLoggingAppService : ILoggingAppService
    {
        private static readonly string[] Applications = ["Dashboard-OP", "Billing-API", "Auth-Service", "Worker-Jobs"];

        private static readonly string[] Messages =
        [
            "Request completed successfully",
            "User signed in",
            "Cache miss for key",
            "Slow database query detected",
            "Retrying external HTTP call",
            "Unhandled exception while processing request",
            "Background job finished",
            "Configuration reloaded"
        ];

        private readonly List<LogEntry> _entries;
        private readonly Lock _lock = new();
        private int _nextId;

        public MockLoggingAppService()
        {
            _entries = CreateSeedData();
            _nextId = _entries.Count + 1;
        }

        public Task<(IReadOnlyList<LogEntry> Items, int TotalCount)> GetAsync(LogQueryDto query, CancellationToken cancellationToken = default)
        {
            lock (_lock)
            {
                IEnumerable<LogEntry> filtered = _entries;

                if (query.MinSeverity is { } minSeverity)
                    filtered = filtered.Where(e => e.Severity >= minSeverity);

                if (!string.IsNullOrWhiteSpace(query.Application))
                    filtered = filtered.Where(e => e.Application.Equals(query.Application, StringComparison.OrdinalIgnoreCase));

                if (query.From is { } from)
                    filtered = filtered.Where(e => e.Timestamp >= from);

                if (query.To is { } to)
                    filtered = filtered.Where(e => e.Timestamp <= to);

                var ordered = filtered.OrderByDescending(e => e.Timestamp).ToList();
                var page = ordered
                    .Skip((query.Page - 1) * query.PageSize)
                    .Take(query.PageSize)
                    .Select(Clone)
                    .ToList();

                return Task.FromResult<(IReadOnlyList<LogEntry>, int)>((page, ordered.Count));
            }
        }

        public Task<LogEntry?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            lock (_lock)
            {
                var entry = _entries.FirstOrDefault(e => e.Id == id);
                return Task.FromResult(entry is null ? null : Clone(entry));
            }
        }

        public Task<LogEntry> AddAsync(LogEntry entry, CancellationToken cancellationToken = default)
        {
            lock (_lock)
            {
                var stored = Clone(entry);
                stored.Id = _nextId++;
                _entries.Add(stored);
                return Task.FromResult(Clone(stored));
            }
        }

        public Task<LogEntry?> UpdateAsync(int id, UpdateLogEntryDto changes, CancellationToken cancellationToken = default)
        {
            lock (_lock)
            {
                var entry = _entries.FirstOrDefault(e => e.Id == id);
                if (entry is null)
                    return Task.FromResult<LogEntry?>(null);

                entry.Severity = changes.Severity;
                entry.Message = changes.Message;
                entry.Exception = changes.Exception;
                return Task.FromResult<LogEntry?>(Clone(entry));
            }
        }

        public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            lock (_lock)
            {
                return Task.FromResult(_entries.RemoveAll(e => e.Id == id) > 0);
            }
        }

        private static LogEntry Clone(LogEntry e) => new()
        {
            Id = e.Id,
            Timestamp = e.Timestamp,
            Severity = e.Severity,
            Application = e.Application,
            Message = e.Message,
            Exception = e.Exception,
            CorrelationId = e.CorrelationId
        };

        private static List<LogEntry> CreateSeedData()
        {
            // Fixed seed so the mock data is the same on every run.
            var random = new Random(42);
            var now = DateTime.UtcNow;
            var severities = Enum.GetValues<LogSeverity>();

            return Enumerable.Range(1, 50).Select(id =>
            {
                var severity = severities[random.Next(severities.Length)];
                return new LogEntry
                {
                    Id = id,
                    Timestamp = now.AddMinutes(-id * 15),
                    Severity = severity,
                    Application = Applications[random.Next(Applications.Length)],
                    Message = Messages[random.Next(Messages.Length)],
                    Exception = severity >= LogSeverity.Error
                        ? "System.InvalidOperationException: Mock failure for demo purposes."
                        : null,
                    CorrelationId = Guid.NewGuid().ToString("N")
                };
            }).ToList();
        }
    }
}
