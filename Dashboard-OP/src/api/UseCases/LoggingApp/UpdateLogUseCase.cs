using Dashboard_OP.src.api.Dtos.LoggingApp;
using Dashboard_OP.src.api.Services.Interfaces;

namespace Dashboard_OP.src.api.UseCases.LoggingApp
{
    public interface IUpdateLogUseCase
    {
        Task<LogEntryDto?> ExecuteAsync(int id, UpdateLogEntryDto dto, CancellationToken cancellationToken = default);
    }

    public class UpdateLogUseCase(ILoggingAppService loggingAppService) : IUpdateLogUseCase
    {
        public async Task<LogEntryDto?> ExecuteAsync(int id, UpdateLogEntryDto dto, CancellationToken cancellationToken = default)
        {
            var updated = await loggingAppService.UpdateAsync(id, dto, cancellationToken);
            return updated?.ToDto();
        }
    }
}
