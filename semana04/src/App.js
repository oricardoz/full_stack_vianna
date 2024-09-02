import React, { useState, useEffect } from "react";
import "./App.css";

function App() {
  const [count, setCount] = useState(0);

  useEffect(() => {
    document.title = `Você clicou ${count} vezes`;

    return () => {
      document.title = "React App";
    };
  }, [count]);

  return (
    <div>
      <h1>Você clicou {count} vezes</h1>
      <button onClick={() => setCount(count + 1)}>Clique aqui</button>
    </div>
  );
}

export default App;
