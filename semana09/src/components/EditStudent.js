import React, { useState, useEffect } from "react";
import axios from "axios";
import { useNavigate, useParams } from "react-router-dom";

const EditStudent = () => {
  const [name, setName] = useState("");
  const { id } = useParams();
  const navigate = useNavigate();

  useEffect(() => {
    axios
      .get(`http://localhost:3001/students/${id}`)
      .then((response) => {
        setName(response.data.name);
      })
      .catch((error) => {
        console.error("Houve um erro ao buscar o aluno:", error);
      });
  }, [id]);

  const handleSubmit = (e) => {
    e.preventDefault();
    axios
      .put(`http://localhost:3001/students/${id}`, { name })
      .then(() => {
        navigate("/");
      })
      .catch((error) => {
        console.error("Houve um erro ao atualizar o aluno:", error);
      });
  };

  return (
    <div>
      <h3>Editar Aluno</h3>
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
        <button type="submit" className="btn btn-warning">
          Atualizar
        </button>
      </form>
    </div>
  );
};

export default EditStudent;
