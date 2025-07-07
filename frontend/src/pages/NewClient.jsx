/* eslint-disable jsx-a11y/label-has-associated-control */
import axios from "axios";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import Cookies from "universal-cookie";

function NewClient() {
  const [form, setForm] = useState({
    nome: "",
    cpf: "",
    email: "",
    phoneNumber: "",
    gender: "",
  });

  const [errors, setErrors] = useState([]);

  const navigate = useNavigate();
  const cookie = new Cookies();
  const jwt = cookie.get("jwt_authorization");

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
    setErrors([]);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      await axios.post("http://localhost:5194/Client", form, {
        headers: { Authorization: `Bearer ${jwt}` },
      });

      navigate("/clients");
    } catch (err) {
      setErrors(err.response.data.errors);
    }
  };

  return (
    <div className="flex items-center justify-center px-4">
      <div className="w-full max-w-lg bg-white rounded-2xl shadow-lg p-8">
        <h2 className="text-3xl font-bold text-center text-gray-800 mb-6">
          Novo Cliente
        </h2>
        <form onSubmit={handleSubmit} className="space-y-5">
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Nome
            </label>
            <input
              name="nome"
              type="text"
              onChange={handleChange}
              required
              className="w-full border border-gray-300 rounded-md p-2 focus:outline-none focus:ring-2 focus:ring-indigo-400"
            />
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              CPF
            </label>
            <input
              name="cpf"
              type="text"
              onChange={handleChange}
              required
              className="w-full border border-gray-300 rounded-md p-2 focus:outline-none focus:ring-2 focus:ring-indigo-400"
            />
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Email
            </label>
            <input
              name="email"
              type="email"
              onChange={handleChange}
              className="w-full border border-gray-300 rounded-md p-2 focus:outline-none focus:ring-2 focus:ring-indigo-400"
            />
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Telefone
            </label>
            <input
              name="phoneNumber"
              type="text"
              onChange={handleChange}
              className="w-full border border-gray-300 rounded-md p-2 focus:outline-none focus:ring-2 focus:ring-indigo-400"
            />
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Gênero
            </label>
            <select
              name="gender"
              onChange={handleChange}
              required
              className="w-full border border-gray-300 rounded-md p-2 focus:outline-none focus:ring-2 focus:ring-indigo-400"
            >
              <option value="">Selecione o gênero</option>
              <option value="M">Masculino</option>
              <option value="F">Feminino</option>
              <option value="I">Outro</option>
            </select>
          </div>

          {errors.length > 0 && (
            <div className="bg-red-100 rounded-md">
              {errors.map((e, i) => {
                return (
                  <div className="text-red-600 p-1 text-center" key={i}>
                    {e}
                  </div>
                );
              })}
            </div>
          )}

          <div className="flex gap-2">
            <button
              className="w-full bg-red-500 hover:bg-red-600 text-white font-bold py-2 rounded-md shadow-md transition duration-200"
              onClick={() => navigate("/clients")}
            >
              Cancelar
            </button>
            <button
              type="submit"
              className="w-full bg-green-500 hover:bg-green-600 text-white font-bold py-2 rounded-md shadow-md transition duration-200"
            >
              Salvar Cliente
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}

export default NewClient;
