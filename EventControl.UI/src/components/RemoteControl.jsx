import React, { useEffect, useState } from "react";

// Importo las funciones para conectarme al Hub y mandar comandos
import {
  connectToSignalR,
  sendOpenFormCommand,
  sendCloseClientCommand,
} from "../services/signalRService";

//Componente que controla el cliente WinForms desde la página de React
function RemoteControl({ reloadEventLogs }) {
  //Guardo si el formulario WinForms está abierto o no
  const [isSecondaryOpen, setIsSecondaryOpen] = useState(false);

  //Al cargar el componente, me conecto al Hub y le digo qué hacer
  useEffect(() => {
    connectToSignalR(
      //Si llega el comando para abrir, actualizo el estado
      (msg) => {
        console.log("Formulario abierto con mensaje:", msg);
        setIsSecondaryOpen(true);
      },

      //Si llega el comando para cerrar, actualizo el estado
      () => {
        console.log("Cliente cerrado por el servidor");
        setIsSecondaryOpen(false);
      }
    );
  }, []);

  //Función que manda el comando para abrir el formulario
  const openWinForms = async () => {
    try {
      await sendOpenFormCommand("Formulario abierto desde React");
      setTimeout(() => {
        reloadEventLogs?.();
      }, 1000);
    } catch (err) {
      console.error("Error al abrir WinForms:", err);
      alert("Error al abrir WinForms");
    }
  };

  return (
    <div>
      <h2>Remote Client Control</h2>
      {/* Botón para abrir el formulario secundario */}
      <button onClick={openWinForms}>Abrir Secondary Form</button>
      {isSecondaryOpen && (
        <button
          onClick={() => {
            sendCloseClientCommand();   //Comando al backend para cerrar el cliente
            setIsSecondaryOpen(false);
            setTimeout(() => {
              reloadEventLogs?.();
            }, 1000);
          }}
        >
          Cerrar Secondary Form
        </button>
      )}
    </div>
  );
}

export default RemoteControl;
