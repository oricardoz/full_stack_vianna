import React, { useState, useContext, createContext } from "react";
import "./App.css";

const ThemeContext = createContext();

function App() {
  const [theme, setTheme] = useState("light");

  const toggleTheme = () => {
    setTheme((prevTheme) => (prevTheme === "light" ? "dark" : "light"));
  };

  return (
    <ThemeContext.Provider value={{ theme, toggleTheme }}>
      <div className={`App ${theme}`}>
        <header className="App-header">
          <h1>App com Tema {theme === "light" ? "Claro" : "Escuro"}</h1>
          <ThemeButton />
        </header>
      </div>
    </ThemeContext.Provider>
  );
}

function ThemeButton() {
  const { theme, toggleTheme } = useContext(ThemeContext);

  return (
    <button onClick={toggleTheme}>
      Mudar para tema {theme === "light" ? "escuro" : "claro"}
    </button>
  );
}

export default App;
