/* eslint-disable import/no-unresolved */
import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useUser } from "../contexts/UserContext";
import { CreateNewUser } from "../hooks/UseGetUser";

function Register() {
  const [form, setForm] = useState({
    name: "",
    email: "",
    password: "",
    cnpj: "",
    address: "",
  });

  const { login } = useUser(); //Destructuring

  const handleChange = (e) => {
    setForm({ ...form, [e.target.id.replace("input", "")]: e.target.value });
  };
  const navigate = new useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault(); // 🚨 ESSENCIAL para evitar o reload da página!

    try {
      const res = await CreateNewUser(form);
      await login(res.accessToken);
      navigate("/");
    } catch {
      alert("Erro ao criar usuário");
    }
  };

  return (
    <div className="flex flex-col items-center justify-center">
      <span className="text-4xl font-bold">Create your account</span>
      <div className="w-[40%]">
        <form onSubmit={handleSubmit}>
          {["name", "email", "password", "cnpj", "address"].map((field) => (
            <div key={field}>
              <label className="text-lg mt-8" htmlFor={`${field}input`}>
                {field.charAt(0).toUpperCase() + field.slice(1)}
              </label>
              <input
                type={field === "password" ? "password" : "text"}
                id={`${field}input`}
                placeholder={`${field.charAt(0).toUpperCase() + field.slice(1)}...`}
                value={form[field]}
                onChange={handleChange}
                className="block w-full p-4 ps-4 text-sm text-black border border-gray-300 rounded-lg 
                        bg-gray-50 dark:bg-gray-100 dark:border-gray-600 
                        placeholder-gray-500 h-10 mt-1 mb-6"
              />
            </div>
          ))}

          <button
            type="submit"
            className="font-bold text-lg hover:bg-blue-800 cursor-pointer 
                        hover:transition hover:duration-300 bg-blue-700
                        rounded-md py-2 px-4 text-white w-[100%] mt-4"
          >
            Create Account
          </button>
        </form>

        <div className="flex justify-end mt-2">
          <Link
            to="/login"
            className="text-gray-500 hover:text-gray-800 hover:underline"
          >
            Already have an account? Login
          </Link>
        </div>
      </div>
    </div>
  );
}

export default Register;
