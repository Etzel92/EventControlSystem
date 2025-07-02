using EventClient.App.Data;
using System;
using System.Net;
using System.Windows.Forms;

namespace EventClient.App.Services
{
    public class EventService
    {
        //Valida si el formulario sigue abierto o no
        public bool IsFormOpen => _secondaryForm != null && !_secondaryForm.IsDisposed;

        // Aquí guardo la referencia al formulario
        private SecondaryForm? _secondaryForm;

        //Guardo el Id del evento que se abrió
        private int _currentEventId = -1;

        //Método para abir el formulario, guarda el evento en la base y enviar el mensaje
        public void OpenSecondaryForm(string message, string source, string eventType)
        {
            //Valida si ya está abierto apara no abrirlo de nuevo
            if (_secondaryForm != null && !_secondaryForm.IsDisposed)
                return;

            //Consigue la hora actual y el nombre del equipo
            var now = DateTime.Now;
            string host = Dns.GetHostName();

            //Inserta el evento y guarda el ID
            _currentEventId = EventLogger.InsertEvent(now, source, eventType, host);

            //Crea el formulario y le pasa el mensaje para que lo muestre
            _secondaryForm = new SecondaryForm();
            _secondaryForm.DisplayMessage = message;

            //Cuando se cierre el formulario, automáticamente se registra el cierre del evento
            _secondaryForm.FormClosed += (s, e) =>
            {
                EventLogger.CloseEvent(_currentEventId, DateTime.Now, "Cerrado Manualmente");
                _currentEventId = -1;
            };

            //Muestra el formulario en pantalla
            _secondaryForm.Show();
        }

        //Método ppara cerrar el formulario desde react con signalr
        public void CloseSecondaryForm(string eventType)
        {
            // Solo lo cierra si todavía está abierto
            if (_secondaryForm != null && !_secondaryForm.IsDisposed)
            {
                // Registra el cierre en la base y luego cierra el formulario
                EventLogger.CloseEvent(_currentEventId, DateTime.Now, eventType);
                _currentEventId = -1;
                _secondaryForm.Close();
            }
        }
    }
}
