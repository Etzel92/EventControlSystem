using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventClient.App.Data
{
    internal class EventLogger
    {
        //Conexión a la base de datos
        private static readonly string connectionString =
            "Server=localhost;Database=RetailEventsDB;Trusted_Connection=True;TrustServerCertificate=True;";

        //Método para registrar un evento cuando se abre el formulario llmando al SP InsertEventLog
        public static int InsertEvent(DateTime openingTime, string openingSource, string eventType, string hostName)
        {
            //Conexión con SQL Server
            using var conn = new SqlConnection(connectionString);

            //Se prepara el comando para ejecutar el SP InsertEventLog
            using var cmd = new SqlCommand("InsertEventLog", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            //Se agregan los datos que se van a guardar en la base
            cmd.Parameters.AddWithValue("@OpeningTime", openingTime);
            cmd.Parameters.AddWithValue("@OpeningSource", openingSource);
            cmd.Parameters.AddWithValue("@EventType", eventType);
            cmd.Parameters.AddWithValue("@HostName", hostName);

            //Se abre la conexión y se ejecuta el comando
            conn.Open();

            //Ejecuta el SP y devuelve el ID del nuevo registro
            var newId = Convert.ToInt32(cmd.ExecuteScalar());
            return newId;
        }

        //Método para cerrar el formulario llamando a CloseEventLog
        public static void CloseEvent(int id, DateTime closingTime, string eventType)
        {
            using var conn = new SqlConnection(connectionString);

            // Prepara el comando para el SP CloseEventLog
            using var cmd = new SqlCommand("CloseEventLog", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            // Se agregan los datos para actualizar el evento
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@ClosingTime", closingTime);
            cmd.Parameters.AddWithValue("@EventType", eventType);

            // Ejecuta la actualización
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
