// Importa el namespace para trabajar con Entity Framework Core
using Microsoft.EntityFrameworkCore;

using EventControl.API.Models;

namespace EventControl.API.Data
{
    // Hereda de DbContext, para permitir que EF maneje las operaciones con SQL Server.
    public class EventLogDbContext : DbContext
    {
        // Constructor que recibe las opciones del context.
        public EventLogDbContext(DbContextOptions<EventLogDbContext> options)
            : base(options) { }

        // DbSet la tabla EventLog en la base de datos.
        public DbSet<EventLog> EventLogs => Set<EventLog>();
    }
}
