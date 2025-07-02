using Microsoft.AspNetCore.SignalR.Client;

public class SignalRClient
{
    //Se guarda la conexión con el Hub
    private readonly HubConnection _connection;

    //Acciones que se ejecutan cuando llegan los comandos del servidor
    public Action<string>? OnOpenFormReceived;
    public Action? OnCloseClientReceived;

    public SignalRClient(string hubUrl)
    {
        _connection = new HubConnectionBuilder()
            .WithUrl(hubUrl)
            .WithAutomaticReconnect()
            .Build();

        //Si se recibe el comando se ejecuta la acción correspondientes
        _connection.On<string>("ReceiveOpenFormCommand", (message) =>
        {
            OnOpenFormReceived?.Invoke(message);
        });

        _connection.On("ReceiveCloseClientCommand", () =>
        {
            OnCloseClientReceived?.Invoke();
        });
    }

    // Este método arranca la conexión con el Hub
    public async Task StartAsync()
    {
        await _connection.StartAsync();
    }

    //Valida si la conexipon con el hub esta activa o no.
    public bool IsConnected => _connection.State == HubConnectionState.Connected;
}
