import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import ListStudents from "./components/ListStudents";
import AddStudent from "./components/AddStudent";
import EditStudent from "./components/EditStudent";
import DeleteStudent from "./components/DeleteStudent";

function App() {
  return (
    <Router>
      <div className="container">
        <h2 className="my-4 text-center">CRUD de Alunos</h2>
        <Routes>
          <Route path="/" element={<ListStudents />} />
          <Route path="/add" element={<AddStudent />} />
          <Route path="/edit/:id" element={<EditStudent />} />
          <Route path="/delete/:id" element={<DeleteStudent />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
