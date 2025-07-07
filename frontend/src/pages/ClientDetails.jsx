import axios from "axios";
import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import Cookies from "universal-cookie";

function ClientDetails() {
  const { id } = useParams();
  const [client, setClient] = useState(null);
  const [isEditing, setIsEditing] = useState(false);
  const [form, setForm] = useState({});
  const cookie = new Cookies();
  const jwt = cookie.get("jwt_authorization");
  const navigate = useNavigate();

  useEffect(() => {
    const fetchClient = async () => {
      try {
        const res = await axios.get(`http://localhost:5194/Client/${id}`, {
          headers: { Authorization: `Bearer ${jwt}` },
        });
        setClient(res.data);
        setForm(res.data);
      } catch (err) {
        console.error("Erro ao buscar cliente:", err);
      }
    };

    fetchClient();
  }, [id]);

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const MostrarGeneroInteiro = (string) => {
    if (string == "M") return "Masculino";
    else if (string == "F") return "Feminino";
    else if (string == "I") return "Outro";
    else return string;
  };

  const handleSave = async () => {
    try {
      await axios.patch(`http://localhost:5194/Client/${id}`, form, {
        headers: { Authorization: `Bearer ${jwt}` },
      });
      setClient(form);
      setIsEditing(false);
    } catch (err) {
      console.error("Erro ao atualizar cliente:", err);
    }
  };

  if (!client) return <div className="p-6">Carregando...</div>;

  return (
    <div className="p-6 flex items-center justify-center">
      <div className="bg-white rounded-2xl shadow-lg p-8 w-full max-w-2xl">
        <h2 className="text-2xl font-bold text-indigo-700 mb-6 text-center">
          Detalhes do Cliente
        </h2>

        <div className="space-y-4">
          {["name", "cpf", "email", "phoneNumber", "gender"].map((field) => (
            <div key={field}>
              <label className="block text-sm font-medium text-indigo-700 capitalize">
                {field === "cpf"
                  ? "CPF"
                  : field === "phoneNumber"
                    ? "Telefone"
                    : field}
              </label>
              {isEditing ? (
                field === "gender" ? (
                  <select
                    name={field}
                    value={form[field] || ""}
                    onChange={handleChange}
                    className="w-full border rounded-md p-2 mt-1"
                  >
                    <option value="">Selecione o gênero</option>
                    <option value="M">Masculino</option>
                    <option value="F">Feminino</option>
                    <option value="I">Outro</option>
                  </select>
                ) : (
                  <input
                    name={field}
                    value={form[field] || ""}
                    onChange={handleChange}
                    className="w-full border rounded-md p-2 mt-1"
                  />
                )
              ) : (
                <p className="text-gray-800 font-medium mt-1">
                  {MostrarGeneroInteiro(client[field]) || "Não informado"}
                </p>
              )}
            </div>
          ))}
        </div>

        <div className="mt-6 flex justify-end space-x-4">
          {isEditing ? (
            <>
              <button
                onClick={handleSave}
                className="bg-green-500 hover:bg-green-600 text-white font-semibold px-4 py-2 rounded-md"
              >
                Salvar
              </button>
              <button
                onClick={() => {
                  setForm(client);
                  setIsEditing(false);
                }}
                className="bg-gray-300 hover:bg-gray-400 text-gray-800 font-semibold px-4 py-2 rounded-md"
              >
                Cancelar
              </button>
            </>
          ) : (
            <div className="flex gap-2">
              <button
                onClick={() => navigate("/clients")}
                className="bg-yellow-500 hover:bg-yellow-600 text-white font-semibold px-4 py-2 rounded-md"
              >
                Voltar
              </button>
              <button
                onClick={() => setIsEditing(true)}
                className="bg-indigo-500 hover:bg-indigo-600 text-white font-semibold px-4 py-2 rounded-md"
              >
                Editar
              </button>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}

export default ClientDetails;
