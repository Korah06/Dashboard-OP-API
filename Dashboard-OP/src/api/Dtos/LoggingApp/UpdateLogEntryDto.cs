using System.ComponentModel.DataAnnotations;
using Dashboard_OP.src.api.Models;

namespace Dashboard_OP.src.api.Dtos.LoggingApp
{
    public class UpdateLogEntryDto
    {
        [Required]
        public LogSeverity Severity { get; set; }

        [Required, StringLength(2000)]
        public string Message { get; set; } = string.Empty;

        public string? Exception { get; set; }
    }
}
