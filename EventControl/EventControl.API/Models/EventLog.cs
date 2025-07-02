using System.ComponentModel.DataAnnotations.Schema;

namespace EventControl.API.Models
{
    // Indica que se usará la tabla a "EventLog"
    [Table("EventLog")]
    public class EventLog
    {
        public int Id { get; set; }
        public DateTime OpeningTime { get; set; }
        // Usamor el signo de interrogación para indicar que es opcional (nullable),
        public DateTime? ClosingTime { get; set; }
        public string? Comment { get; set; }
        // Inicializamos como cadena vacía para evitar nulos.
        public string OpeningSource { get; set; } = string.Empty;
        public string EventType { get; set; } = string.Empty;
        public string HostName { get; set; } = string.Empty;
    }
}

