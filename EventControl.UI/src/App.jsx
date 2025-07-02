import React, { useRef } from "react";
import RemoteControl from "./components/RemoteControl";
import EventLogTable from "./components/EventLogTable";
import "./App.css";

function App() {
  //Crea una referencia para poder controlar el componente EventLogTable desde otro componente
  const eventLogRef = useRef();

  return (
    <div className="App">
      <div className="container">
        <h1 className="main-title">Event Control UI</h1>

        {/*Sección donde va el control remoto*/}
        <div className="controls-container">
          {/*Le paso la función que permite recargar la tabla desde RemoteControl*/}
          <RemoteControl
            reloadEventLogs={() => eventLogRef.current?.loadLogs()}
          />
        </div>

        <hr className="divider" />

        {/*Sección donde va la tabla*/}
        <div className="table-container">
          {/*Le asigno la referencia al componente de la tabla para poder acceder a su función desde afuera */}
          <EventLogTable ref={eventLogRef} />
        </div>
      </div>
    </div>
  );
}

export default App;
