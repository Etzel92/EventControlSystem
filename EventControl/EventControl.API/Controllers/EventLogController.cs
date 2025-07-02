using EventControl.API.Data;
using EventControl.API.Models;
// Importa herramientas para construir controladores HTTP
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventControl.API.Controllers
{
    [ApiController]

    // Define la ruta base del controlador: "api/EventLog"
    [Route("api/[controller]")]
    public class EventLogController : ControllerBase
    {
        // Contexto de base de datos para acceder a la tabla EventLogs
        private readonly EventLogDbContext _context;

        // Constructor que recibe el contexto por inyección de dependencias
        public EventLogController(EventLogDbContext context)
        {
            _context = context;
        }

        // Endpoint GET que devuelve todos los registros de la tabla EventLog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventLog>>> Get()
        {
            // Obtiene la lista completa de registros de la BD
            var logs = await _context.EventLogs.ToListAsync();
            return Ok(logs);
        }

        // Endpoint PUT que actualiza un registro específico según su ID
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EventLog updated)
        {
            // Busca el registro en la base de datos por ID
            var existing = await _context.EventLogs.FindAsync(id);

            // Si no lo encuentra, devuelve 404 Not Found
            if (existing == null)
                return NotFound();

            // Actualiza los campos del registro con los nuevos valores del cuerpo de la petición
            existing.OpeningTime = updated.OpeningTime;
            existing.ClosingTime = updated.ClosingTime;
            existing.Comment = updated.Comment;
            existing.OpeningSource = updated.OpeningSource;
            existing.EventType = updated.EventType;
            existing.HostName = updated.HostName;

            // Guarda los cambios en la base de datos
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
