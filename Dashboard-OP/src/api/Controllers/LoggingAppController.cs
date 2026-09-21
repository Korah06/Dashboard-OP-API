using Dashboard_OP.src.api.Dtos;
using Dashboard_OP.src.api.Dtos.LoggingApp;
using Dashboard_OP.src.api.UseCases.LoggingApp;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard_OP.src.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggingAppController(
        IGetLogsUseCase getLogsUseCase,
        IGetLogByIdUseCase getLogByIdUseCase,
        ICreateLogUseCase createLogUseCase,
        IUpdateLogUseCase updateLogUseCase,
        IDeleteLogUseCase deleteLogUseCase) : ControllerBase
    {
        // GET: api/LoggingApp?minSeverity=Warning&application=Billing-API&page=1&pageSize=20
        [HttpGet]
        public async Task<ActionResult<PagedResultDto<LogEntryDto>>> Get([FromQuery] LogQueryDto query, CancellationToken cancellationToken)
        {
            return Ok(await getLogsUseCase.ExecuteAsync(query, cancellationToken));
        }

        // GET api/LoggingApp/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<LogEntryDto>> Get(int id, CancellationToken cancellationToken)
        {
            var entry = await getLogByIdUseCase.ExecuteAsync(id, cancellationToken);
            return entry is null ? NotFound() : Ok(entry);
        }

        // POST api/LoggingApp
        [HttpPost]
        public async Task<ActionResult<LogEntryDto>> Post([FromBody] CreateLogEntryDto dto, CancellationToken cancellationToken)
        {
            var created = await createLogUseCase.ExecuteAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        // PUT api/LoggingApp/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<LogEntryDto>> Put(int id, [FromBody] UpdateLogEntryDto dto, CancellationToken cancellationToken)
        {
            var updated = await updateLogUseCase.ExecuteAsync(id, dto, cancellationToken);
            return updated is null ? NotFound() : Ok(updated);
        }

        // DELETE api/LoggingApp/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            return await deleteLogUseCase.ExecuteAsync(id, cancellationToken) ? NoContent() : NotFound();
        }
    }
}
