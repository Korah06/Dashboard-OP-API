using Dashboard_OP.src.api.Dtos;
using Dashboard_OP.src.api.Dtos.LoggingApp;
using Dashboard_OP.src.api.Services.Interfaces;

namespace Dashboard_OP.src.api.UseCases.LoggingApp
{
    public interface IGetLogsUseCase
    {
        Task<PagedResultDto<LogEntryDto>> ExecuteAsync(LogQueryDto query, CancellationToken cancellationToken = default);
    }

    public class GetLogsUseCase(ILoggingAppService loggingAppService) : IGetLogsUseCase
    {
        public async Task<PagedResultDto<LogEntryDto>> ExecuteAsync(LogQueryDto query, CancellationToken cancellationToken = default)
        {
            var (items, totalCount) = await loggingAppService.GetAsync(query, cancellationToken);
            return new PagedResultDto<LogEntryDto>(
                items.Select(e => e.ToDto()).ToList(),
                query.Page,
                query.PageSize,
                totalCount);
        }
    }
}
