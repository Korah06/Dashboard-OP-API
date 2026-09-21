using Dashboard_OP.src.api.Dtos.LoggingApp;
using Dashboard_OP.src.api.Services.Interfaces;

namespace Dashboard_OP.src.api.UseCases.LoggingApp
{
    public interface IGetLogByIdUseCase
    {
        Task<LogEntryDto?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
    }

    public class GetLogByIdUseCase(ILoggingAppService loggingAppService) : IGetLogByIdUseCase
    {
        public async Task<LogEntryDto?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entry = await loggingAppService.GetByIdAsync(id, cancellationToken);
            return entry?.ToDto();
        }
    }
}
