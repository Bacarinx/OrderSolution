/* eslint-disable jsx-a11y/label-has-associated-control */
import axios from "axios";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router";
import { ToastContainer, toast } from "react-toastify";
import Cookie from "universal-cookie";
import "react-toastify/dist/ReactToastify.css";

function Profile() {
  const [user, setUser] = useState({
    name: "",
    email: "",
    cnpj: "",
    address: "",
  });

  const [isEditing, setIsEditing] = useState(false);
  const navigate = useNavigate();

  const fetchUser = async () => {
    const cookie = new Cookie();
    const jwt = cookie.get("jwt_authorization");

    try {
      const res = await axios.get(`http://localhost:5194/User`, {
        headers: {
          Authorization: `Bearer ${jwt}`,
        },
      });

      setUser({
        name: res.data.name,
        email: res.data.email,
        cnpj: res.data.cnpj,
        address: res.data.address,
      });
    } catch (e) {
      console.log(e);
    }
  };

  const UpdateUser = async () => {
    const cookie = new Cookie();
    const jwt = cookie.get("jwt_authorization");

    try {
      await axios.patch(
        `http://localhost:5194/User`,
        {
          name: user.name,
          email: user.email,
          cnpj: user.cnpj,
          address: user.address,
        },
        {
          headers: {
            Authorization: `Bearer ${jwt}`,
          },
        },
      );

      setIsEditing(false);
      toast.success("Usuário atualizado com sucesso!");

      setTimeout(() => {
        navigate("/");
      }, 2000);
    } catch {
      toast.error("Erro ao atualizar o usuário");
    }
  };

  useEffect(() => {
    fetchUser();
  }, []);

  const handleChange = (e) => {
    setUser({ ...user, [e.target.name]: e.target.value });
  };

  const handleEditToggle = () => {
    setIsEditing(!isEditing);
  };

  const handleSave = () => {
    // Aqui você pode chamar sua API para salvar os dados
    console.log("Salvar dados:", user);
    setIsEditing(false);
  };

  return (
    <div className="max-w-2xl mx-auto bg-white p-6 rounded-xl shadow-md mt-8">
      <h2 className="text-2xl font-bold mb-4 text-gray-700">
        Informações do Usuário
      </h2>
      <form className="space-y-4">
        <div>
          <label className="block text-gray-600 mb-1">Nome</label>
          <input
            type="text"
            name="name"
            value={user.name}
            onChange={handleChange}
            readOnly={!isEditing}
            disabled={!isEditing}
            className={`w-full border rounded-md p-2 bg-gray-50 focus:outline-none focus:ring 
                        focus:border-blue-300 ${!isEditing ? "select-none text-gray-500" : ""}`}
          />
        </div>

        <div>
          <label className="block text-gray-600 mb-1">Email</label>
          <input
            type="email"
            name="email"
            value={user.email}
            onChange={handleChange}
            readOnly={!isEditing}
            disabled={!isEditing}
            className={`w-full border rounded-md p-2 bg-gray-50 focus:outline-none focus:ring 
                        focus:border-blue-300 ${!isEditing ? "select-none text-gray-500" : ""}`}
          />
        </div>

        <div>
          <label className="block text-gray-600 mb-1">CNPJ</label>
          <input
            type="text"
            name="cnpj"
            value={user.cnpj}
            onChange={handleChange}
            readOnly={!isEditing}
            disabled={!isEditing}
            className={`w-full border rounded-md p-2 bg-gray-50 focus:outline-none focus:ring 
                        focus:border-blue-300 ${!isEditing ? "select-none text-gray-500" : ""}`}
          />
        </div>

        <div>
          <label className="block text-gray-600 mb-1">Endereço</label>
          <input
            type="text"
            name="address"
            value={user.address}
            onChange={handleChange}
            readOnly={!isEditing}
            disabled={!isEditing}
            className={`w-full border rounded-md p-2 bg-gray-50 focus:outline-none focus:ring 
                        focus:border-blue-300 ${!isEditing ? "select-none text-gray-500" : ""}`}
          />
        </div>

        <div className="flex justify-end gap-3 pt-4">
          {isEditing ? (
            <>
              <button
                type="button"
                onClick={UpdateUser}
                className="px-4 py-2 bg-green-600 text-white rounded-md hover:bg-green-700"
              >
                Salvar
              </button>
              <button
                type="button"
                onClick={handleEditToggle}
                className="px-4 py-2 bg-gray-300 text-gray-800 rounded-md hover:bg-gray-400"
              >
                Cancelar
              </button>
            </>
          ) : (
            <button
              type="button"
              onClick={handleEditToggle}
              className="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700"
            >
              Editar
            </button>
          )}
        </div>
      </form>
      <ToastContainer
        position="top-right"
        autoClose={2000}
        hideProgressBar={true}
      ></ToastContainer>
    </div>
  );
}

export default Profile;
