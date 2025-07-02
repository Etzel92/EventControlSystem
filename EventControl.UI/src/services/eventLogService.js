//Importo axios para hacer peticiones al backend
import axios from "axios";

//Esta es la URL base de la API
const API_URL = "https://localhost:7290/api/eventlog";

//Función para traer todos los registros desde el backend
export const getEventLogs = async () => {
  const response = await axios.get(API_URL); // Llama al GET de la API
  return response.data; // Devuelve los datos en formato JSON
};

//Función para actualizar un registro específico
export const updateEventLog = async (id, updatedData) => {
  await axios.put(`${API_URL}/${id}`, updatedData); // Llama al PUT de la API con el ID del evento
};
