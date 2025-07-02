using EventClient.App.Services;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Net;
using System.Windows.Forms;

namespace EventClient.App
{
    public partial class MainForm : Form
    {
        //Guarda la conexión con SignalR
        private readonly SignalRClient _signalRClient;

        //Guarda el servicio que se encarga de abrir, cerrar y registrar eventos
        private readonly EventService _eventService;

        public MainForm()
        {
            InitializeComponent();

            //Crea el servicio para manejar el formulario y los eventos
            _eventService = new EventService();

            //Crea el cliente SignalR y le paso la URL del hub
            _signalRClient = new SignalRClient("https://localhost:7290/hubs/winforms");

            //Al llegar el comando de abrir, muestra el formulario y lo registro
            _signalRClient.OnOpenFormReceived = (message) =>
            {
                //Se usa Invoke porque SignalR corre en otro hilo distinto a la interfaz
                Invoke(() => _eventService.OpenSecondaryForm(message, "Remoto", "Abierto Remotamente"));
            };

            //Al llegar el comando de cerrar, se cierra el formulario si esta abierto
            _signalRClient.OnCloseClientReceived = () =>
            {
                Invoke(() =>
                {
                    if (_eventService.IsFormOpen)
                    {
                        _eventService.CloseSecondaryForm("Cerrado Remotamente");
                    }
                });
            };

            //Arranca la conexión con SignalR
            StartSignalR();
        }

        //Método que intenta conectar con el hub y muestra mensaje si falla
        private async void StartSignalR()
        {
            try
            {
                await _signalRClient.StartAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con SignalR: " + ex.Message);
            }
        }

        //Boton para abrir el formulario manualmente
        private void btnOpenManual_Click(object sender, EventArgs e)
        {
            _eventService.OpenSecondaryForm("Formulario abierto manualmente", "Local", "Abierto Localmente");
        }
    }
}
