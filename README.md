# EventControlSystem

**EventControlSystem** es una solución distribuida que permite la gestión y monitoreo de eventos en tiempo real entre una interfaz web administrativa y múltiples aplicaciones cliente de escritorio. El sistema consta de tres componentes principales:

- **EventControl.UI**: Frontend en React para el control remoto y monitoreo de eventos.
- **EventControl.API**: Backend ASP.NET Core con endpoints REST y Hub SignalR para comunicación en tiempo real.
- **EventClient.App**: Aplicación de escritorio en Windows Forms que responde a comandos remotos y registra eventos en base de datos.

---

## 🧩 Arquitectura del sistema

EventControlSystem implementa una arquitectura de tres capas:

```
[EventControl.UI] ⇄ [EventControl.API + SignalR Hub] ⇄ [EventClient.App]
```

- El administrador desde la UI web puede abrir/cerrar formularios en los clientes WinForms.
- El backend transmite los comandos mediante SignalR y registra/consulta eventos en SQL Server.
- Los clientes WinForms reciben los comandos y registran su actividad (manual o remota).

---

## 📦 Componentes

| Componente         | Descripción |
|--------------------|-------------|
| **EventControl.UI** | React + Vite + SignalR client. Interfaz para enviar comandos y consultar bitácora. |
| **EventControl.API** | ASP.NET Core, Entity Framework y SignalR. Expone endpoints REST y concentra mensajes en tiempo real. |
| **EventClient.App** | Aplicación WinForms (.NET 8.0) que muestra formularios y registra eventos según comandos recibidos. |
| **SQL Server** | Base de datos con procedimientos almacenados para registrar apertura y cierre de eventos. |

---

## 🚀 Funcionalidades principales

- ✅ Control remoto de formularios cliente vía SignalR
- ✅ Registro automático de eventos con hora, origen, tipo y equipo
- ✅ Bitácora editable desde la interfaz web (campo comentario)
- ✅ Comunicación en tiempo real entre React, API y cliente WinForms
- ✅ Arquitectura modular: componentes independientes pero integrados

---

## 🛠️ Tecnologías utilizadas

- **Frontend**: React 19, Vite, JavaScript, SignalR Client
- **Backend**: ASP.NET Core, Entity Framework Core, SignalR
- **Cliente**: WinForms (.NET 8.0), SignalR Client
- **Base de datos**: SQL Server

---

## 🧪 Cómo ejecutar el proyecto

### 1. Clonar los repositorios
```bash
git clone https://github.com/tuusuario/EventControl.UI.git
git clone https://github.com/tuusuario/EventControl.API.git
git clone https://github.com/tuusuario/EventClient.App.git
```

### 2. Ejecutar el backend
- Configurar `appsettings.json` con la cadena de conexión a SQL Server.
- Ejecutar `EventControl.API` desde Visual Studio o CLI.

### 3. Ejecutar el cliente WinForms
- Abrir `EventClient.sln` y compilar.
- Ejecutar `MainForm` como cliente receptor.

### 4. Ejecutar la UI web
```bash
cd EventControl.UI
npm install
npm run dev
```

> Asegúrate de que las URLs coincidan con las del backend (`localhost:7290` por defecto).

---

## 📚 Casos de uso típicos

- Un administrador desea abrir remotamente un formulario en una terminal específica.
- Se requiere registrar automáticamente cuándo y cómo fue abierto/cerrado un cliente.
- Consultar el historial de eventos, agregar comentarios y auditar actividad.

---

© 2025 Miguel Etzel García Delgado