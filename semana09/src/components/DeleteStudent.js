import React, { useEffect } from "react";
import axios from "axios";
import { useNavigate, useParams } from "react-router-dom";

const DeleteStudent = () => {
  const { id } = useParams();
  const navigate = useNavigate();

  useEffect(() => {
    const confirmDelete = window.confirm(
      "Você tem certeza que deseja excluir este aluno?"
    );
    if (confirmDelete) {
      axios
        .delete(`http://localhost:3001/students/${id}`)
        .then(() => {
          navigate("/");
        })
        .catch((error) => {
          console.error("Houve um erro ao excluir o aluno:", error);
        });
    } else {
      navigate("/");
    }
  }, [id, navigate]);

  return null;
};

export default DeleteStudent;
