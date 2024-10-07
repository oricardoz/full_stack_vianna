import React, { useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";

const AddStudent = () => {
  const [name, setName] = useState("");
  const navigate = useNavigate();

  const handleSubmit = (e) => {
    e.preventDefault();
    axios
      .post("http://localhost:3001/students", { name })
      .then(() => {
        navigate("/");
      })
      .catch((error) => {
        console.error("Houve um erro ao adicionar o aluno:", error);
      });
  };

  return (
    <div>
      <h3>Adicionar Aluno</h3>
      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label>Nome:</label>
          <input
            type="text"
            className="form-control"
            value={name}
            onChange={(e) => setName(e.target.value)}
            required
          />
        </div>
        <button type="submit" className="btn btn-success">
          Adicionar
        </button>
      </form>
    </div>
  );
};

export default AddStudent;
