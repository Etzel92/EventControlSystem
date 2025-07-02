using EventControl.API.Hubs;
using EventControl.API.Data;
using Microsoft.EntityFrameworkCore;

namespace EventControl.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Aquí se conecta la base de datos usando la cadena que viene en appsettings.json
            builder.Services.AddDbContext<EventLogDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Se agregan los controladores que manejan las rutas de la API
            builder.Services.AddControllers();

            // Activo SignalR para poder mandar mensajes en tiempo real a los clientes
            builder.Services.AddSignalR();

            // Se agrega Swagger para poder probar la API desde el navegador
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Esto es para permitir que el frontend de React se pueda comunicar con el backend sin que el navegador lo bloquee
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials(); // Esto es necesario para que SignalR funcione bien desde el frontend
                });
            });

            // Se construye la app
            var app = builder.Build();

            // Si estamos en desarrollo, se activa Swagger para pruebas
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();
            app.UseHttpsRedirection();
            app.MapControllers();
            app.MapHub<WinFormsHub>("/hubs/winforms");
            app.Run(); // Aquí ya arranca la aplicación
        }
    }
}
