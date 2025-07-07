/* eslint-disable jsx-a11y/label-has-associated-control */
import axios from "axios";
import { useState } from "react";
import { useNavigate } from "react-router";
import Cookie from "universal-cookie";

function NewCategory() {
  const [name, setName] = useState("");
  const [createdCategory, setCreatedCategory] = useState(null);
  const [loading, setLoading] = useState(false);
  const [errors, setErrors] = useState(null);

  const navigate = useNavigate();

  const handleChangeInput = (e) => {
    setErrors(null);
    setCreatedCategory(null);
    setName(e.target.value);
  };

  const handleCreateTab = async (e) => {
    e.preventDefault();
    setLoading(true);

    const cookie = new Cookie();
    const jwt = cookie.get("jwt_authorization");

    try {
      const res = await axios.post(
        "http://localhost:5194/Category",
        {
          name,
        },
        {
          headers: {
            Authorization: `Bearer ${jwt}`,
          },
        },
      );

      setCreatedCategory(res.data);
    } catch (error) {
      setErrors(error.response.data.errors);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="max-w-2xl mx-auto mt-10 p-8 bg-white shadow-md rounded-xl border border-gray-200">
      <h1 className="text-3xl font-bold text-blue-700 mb-6">Nova Comanda</h1>

      <form className="space-y-4" onSubmit={handleCreateTab}>
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Nome da Categoria
          </label>
          <input
            type="text"
            value={name}
            onChange={(e) => handleChangeInput(e)}
            required
            className="w-full border px-4 py-2 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
        </div>

        <div className="flex gap-2">
          <button
            type="submit"
            disabled={loading}
            className={`bg-blue-600 text-white px-6 py-2 rounded-md hover:bg-blue-700 transition ${
              loading ? "opacity-50 cursor-not-allowed" : ""
            }`}
          >
            {loading ? "Criando..." : "Criar Categoria"}
          </button>
          <button
            onClick={() => navigate("/category")}
            className={`bg-red-600 text-white px-6 py-2 rounded-md hover:bg-blue-700 transition`}
          >
            cancelar
          </button>
        </div>
      </form>

      {/* Exibir resultado */}
      {errors ? (
        <div className="mt-6 p-4 border rounded-md bg-gray-50 text-gray-800">
          <p>
            <strong>{errors}</strong>
          </p>
        </div>
      ) : (
        createdCategory && (
          <div className="mt-6 p-4 border rounded-md bg-gray-50 text-gray-800">
            <p>
              <strong>Categoria criada com sucesso!</strong>
            </p>
            <p>
              <strong>Nome da Categoria: {createdCategory}</strong>
            </p>
          </div>
        )
      )}
    </div>
  );
}

export default NewCategory;
