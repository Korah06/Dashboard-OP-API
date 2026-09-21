using Dashboard_OP.src.api.Services.Interfaces;

namespace Dashboard_OP.src.api.UseCases.LoggingApp
{
    public interface IDeleteLogUseCase
    {
        Task<bool> ExecuteAsync(int id, CancellationToken cancellationToken = default);
    }

    public class DeleteLogUseCase(ILoggingAppService loggingAppService) : IDeleteLogUseCase
    {
        public Task<bool> ExecuteAsync(int id, CancellationToken cancellationToken = default)
            => loggingAppService.DeleteAsync(id, cancellationToken);
    }
}
