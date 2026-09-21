using System.ComponentModel.DataAnnotations;
using Dashboard_OP.src.api.Models;

namespace Dashboard_OP.src.api.Dtos.LoggingApp
{
    public class LogQueryDto
    {
        public LogSeverity? MinSeverity { get; set; }

        public string? Application { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        [Range(1, int.MaxValue)]
        public int Page { get; set; } = 1;

        [Range(1, 100)]
        public int PageSize { get; set; } = 20;
    }
}
