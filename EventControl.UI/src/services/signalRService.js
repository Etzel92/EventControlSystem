// Importo la librería de SignalR para poder conectarme al hub del backend
import * as signalR from "@microsoft/signalr";

let connection = null;

//Función que se encarga de conectar con el Hub y recibir los comandos del backend
export const connectToSignalR = (onOpenForm, onCloseClient) => {
  connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7290/hubs/winforms") // Dirección del Hub del backend
    .withAutomaticReconnect() // Si se desconecta, intenta reconectar solo
    .build();

  //Cuando llega el comando para abrir, ejecuta la función que le pasé
  connection.on("ReceiveOpenFormCommand", onOpenForm);

  //Cuando llega el comando para cerrar
  connection.on("ReceiveCloseClientCommand", onCloseClient);

  //Se intenta conectar con el Hub
  connection
    .start()
    .then(() => console.log("Conectado a SignalR"))
    .catch((err) => console.error("Error al conectar con SignalR:", err));
};

//Función que manda al backend la orden para que se abra el formulario en el cliente WinForms
export const sendOpenFormCommand = async (message) => {
  if (connection && connection.state === "Connected") {
    await connection.invoke("SendOpenFormCommand", message);
  }
};

//Función que manda la orden para que se cierre el cliente WinForms
export const sendCloseClientCommand = async () => {
  if (connection && connection.state === "Connected") {
    await connection.invoke("SendCloseClientCommand");
  }
};
