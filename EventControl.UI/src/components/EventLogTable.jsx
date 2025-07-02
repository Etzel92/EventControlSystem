import React, {
  useEffect,
  useState,
  forwardRef,
  useImperativeHandle,
} from "react";
import { getEventLogs, updateEventLog } from "../services/eventLogService";

// Componente que muestra la tabla y permite editar comentarios
const EventLogTable = forwardRef((props, ref) => {
  //Guarda la lista editable que se muestra
  const [eventLogs, setEventLogs] = useState([]);

  //Copia para comparar si alguien cambio algo
  const [originalLogs, setOriginalLogs] = useState([]);

  //Función que carga los registros del backend
  const loadLogs = async () => {
    const data = await getEventLogs();
    setEventLogs(data);
    setOriginalLogs(data);
  };

  // Expongo la función loadLogs para que React pueda llamarla desde fuera
  useImperativeHandle(ref, () => ({
    loadLogs,
  }));

  //Carga los registros automáticamente al abrir el componente
  useEffect(() => {
    loadLogs();
  }, []);

  //Función que actualiza el comentario
  const handleChange = (id, field, value) => {
    setEventLogs((prev) =>
      prev.map((log) => (log.id === id ? { ...log, [field]: value } : log))
    );
  };

  //Función que guarda todos los comentarios que se hayan cambiado
  const handleSaveAllChanges = async () => {
    try {
      //Se compara cada log con su versión original y guarda solo los que cambiaron
      const logsToUpdate = eventLogs.filter((log, i) => {
        const original = originalLogs[i];
        return log.comment !== original.comment;
      });

      if (logsToUpdate.length === 0) {
        alert("No hay cambios para guardar.");
        return;
      }

      //Se envian todos los cambios
      await Promise.all(
        logsToUpdate.map((log) => updateEventLog(log.id, log))
      );

      alert("Cambios guardados exitosamente.");
    } catch (error) {
      console.error("Error al guardar:", error);
      alert("Error al guardar cambios.");
    }
  };

  return (
    <div>
      <h2>Bitácora de eventos</h2>
      <table border="1">
        <thead>
          <tr>
            <th>ID</th>
            <th>Hora de Apertura</th>
            <th>Hora de Cierre</th>
            <th>Comentario</th>
            <th>Origen de Apertura</th>
            <th>Tipo de Evento</th>
            <th>Nombre del equipo</th>
          </tr>
        </thead>
        <tbody>
          {eventLogs.map((log) => (
            <tr key={log.id}>
              <td>{log.id}</td>
              <td>
                <input
                  type="datetime-local"
                  step="1"
                  value={log.openingTime?.slice(0, 19)}
                  readOnly
                />
              </td>
              <td>
                <input
                  type="datetime-local"
                  step="1"
                  value={log.closingTime?.slice(0, 19) || ""}
                  readOnly
                />
              </td>
              <td>
                <input
                  type="text"
                  value={log.comment || ""}
                  onChange={(e) =>
                    handleChange(log.id, "comment", e.target.value)
                  }
                />
              </td>
              <td>{log.openingSource}</td>
              <td>{log.eventType}</td>
              <td>{log.hostName}</td>
            </tr>
          ))}
        </tbody>
      </table>
      <button onClick={handleSaveAllChanges} style={{ marginTop: "1rem" }}>
        Guardar todos los cambios
      </button>
    </div>
  );
});

export default EventLogTable;



// Lineas para editar la fecha de aperra
// <td>{log.openingTime?.replace("T", " ")?.slice(0, 19)}</td>
// <td>{log.closingTime ? log.closingTime.replace("T", " ").slice(0, 19) : ""}</td>

// Esta línea permitiría hacer editable la fecha de cierre:
// onChange={(e) => handleChange(log.id, "closingTime", e.target.value)}