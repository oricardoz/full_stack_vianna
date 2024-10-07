import React, { useState, useEffect } from "react";
import axios from "axios";
import { Link } from "react-router-dom";

const ListStudents = () => {
  const [students, setStudents] = useState([]);

  useEffect(() => {
    axios
      .get("http://localhost:3001/students")
      .then((response) => {
        setStudents(response.data);
      })
      .catch((error) => {
        console.error("Houve um erro ao buscar os alunos:", error);
      });
  }, []);

  return (
    <div>
      <Link to="/add" className="btn btn-primary mb-3">
        Adicionar Aluno
      </Link>
      <table className="table table-bordered">
        <thead>
          <tr>
            <th>ID</th>
            <th>Nome</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          {students.map((student) => (
            <tr key={student.id}>
              <td>{student.id}</td>
              <td>{student.name}</td>
              <td>
                <Link
                  to={`/edit/${student.id}`}
                  className="btn btn-warning mr-2"
                >
                  Editar
                </Link>
                <Link to={`/delete/${student.id}`} className="btn btn-danger">
                  Excluir
                </Link>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default ListStudents;
