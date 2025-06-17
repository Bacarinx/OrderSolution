/* eslint-disable import/no-unresolved */
import { useState } from "react";
import { Link, useNavigate } from "react-router";
import { useUser } from "../contexts/UserContext";
import { LoginUser } from "../hooks/UseGetUser";

function Login() {
  const [form, setForm] = useState({ email: "", password: "" });
  const navigate = new useNavigate();
  const { login } = useUser(); //Destructuring

  const handleChange = (e) =>
    setForm({ ...form, [e.target.id.replace("input", "")]: e.target.value });

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const res = await LoginUser(form);
      await login(res.accessToken);
      navigate("/");
    } catch {
      console.log("Erro no login! Tente novamente");
    }
  };

  return (
    <div className="flex flex-col items-center justify-center">
      <span className="text-4xl font-bold">Welcome Back!</span>
      <div className="w-[40%]">
        <form onSubmit={handleSubmit}>
          {["email", "password"].map((field) => (
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

          <div className="flex justify-between">
            <Link
              to={"/forgotpassword"}
              className="text-gray-500 hover:text-gray-800 hover:underline"
            >
              Forgot password?
            </Link>
          </div>

          <button
            type="submit"
            className="font-bold text-lg hover:bg-blue-800 cursor-pointer 
                        hover:transition hover:duration-300 bg-blue-700
                        rounded-md py-2 px-4 text-white w-[100%] mt-4"
          >
            Login
          </button>
        </form>
        <div className="flex justify-end mt-2">
          <Link
            to={"/register"}
            className="text-gray-500 hover:text-gray-800 hover:underline"
          >
            Don&apos;t have an account? Sign Up
          </Link>
        </div>
      </div>
    </div>
  );
}

export default Login;
