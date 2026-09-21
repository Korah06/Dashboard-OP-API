using Dashboard_OP.src.api.Dtos.LoggingApp;
using Dashboard_OP.src.api.Services.Interfaces;

namespace Dashboard_OP.src.api.UseCases.LoggingApp
{
    public interface ICreateLogUseCase
    {
        Task<LogEntryDto> ExecuteAsync(CreateLogEntryDto dto, CancellationToken cancellationToken = default);
    }

    public class CreateLogUseCase(ILoggingAppService loggingAppService) : ICreateLogUseCase
    {
        public async Task<LogEntryDto> ExecuteAsync(CreateLogEntryDto dto, CancellationToken cancellationToken = default)
        {
            var created = await loggingAppService.AddAsync(dto.ToModel(), cancellationToken);
            return created.ToDto();
        }
    }
}
