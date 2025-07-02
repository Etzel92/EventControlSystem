using Microsoft.AspNetCore.SignalR;

namespace EventControl.API.Hubs
{
    public class WinFormsHub : Hub
    {
        //Envía un mensaje para abrir el formulario
        public async Task SendOpenFormCommand(string message)
        {
            await Clients.All.SendAsync("ReceiveOpenFormCommand", message);
        }

        //Envía un mensaje apara cerrar el secondaryform
        public async Task SendCloseClientCommand()
        {
            await Clients.All.SendAsync("ReceiveCloseClientCommand");
        }
    }
}
