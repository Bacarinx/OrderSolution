/* eslint-disable jsx-a11y/click-events-have-key-events */
/* eslint-disable jsx-a11y/no-static-element-interactions */
import axios from "axios";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import Cookies from "universal-cookie";

function Clients() {
  const [clients, setClients] = useState([]);
  const cookie = new Cookies();
  const jwt = cookie.get("jwt_authorization");
  const navigate = useNavigate();

  useEffect(() => {
    const fetchClients = async () => {
      try {
        const res = await axios.get("http://localhost:5194/Client", {
          headers: { Authorization: `Bearer ${jwt}` },
        });
        setClients(res.data);
      } catch (err) {
        console.error("Erro ao buscar clientes:", err);
      }
    };

    fetchClients();
  }, []);

  return (
    <div className="p-6 bg-gray-50 min-h-screen">
      <div className="flex justify-between items-center mb-6">
        <h2 className="text-2xl font-bold text-indigo-500 ">
          Clientes Cadastrados
        </h2>
        <button
          onClick={() => navigate("new-client")}
          className="bg-indigo-500 hover:bg-indigo-600 text-white px-4 py-2 rounded-lg shadow"
        >
          + Novo Cliente
        </button>
      </div>

      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-4">
        {clients.map((client) => (
          <div
            key={client.id}
            onClick={() => navigate(`/clients/${client.id}`)}
            className="bg-white p-4 rounded-xl shadow hover:shadow-md cursor-pointer transition"
          >
            <h3 className="text-lg font-semibold text-gray-800">
              {client.name}
            </h3>
            <p className="text-gray-600 mt-1">CPF: {client.cpf}</p>
          </div>
        ))}
      </div>
    </div>
  );
}

export default Clients;
