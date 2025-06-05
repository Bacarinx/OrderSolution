import { Link } from "react-router";

function Register() {
  return (
    <div className="flex flex-col items-center justify-center">
      <span className="text-4xl font-bold">Create your account</span>
      <div className="w-[40%]">
        <form>
          <label className="text-lg mt-8" htmlFor="nameinput">
            Name
          </label>
          <input
            type="text"
            id="nameinput"
            placeholder="Name..."
            className="block w-full p-4 ps-4 text-sm text-black border border-gray-300 rounded-lg 
                    bg-gray-50 dark:bg-gray-100 dark:border-gray-600 
                    placeholder-gray-500 h-10 mt-1 mb-6"
          />
          <label className="text-lg mt-8" htmlFor="emailinput">
            Email
          </label>
          <input
            type="email"
            id="emailinput"
            placeholder="Email..."
            className="block w-full p-4 ps-4 text-sm text-black border border-gray-300 rounded-lg 
                    bg-gray-50 dark:bg-gray-100 dark:border-gray-600 
                    placeholder-gray-500 h-10 mt-1 mb-6"
          />
          <label className="text-lg mt-8" htmlFor="passwordinput">
            Password
          </label>
          <input
            type="password"
            id="passwordinput"
            placeholder="Password..."
            className="block w-full p-4 ps-4 text-sm text-black border border-gray-300 rounded-lg 
                    bg-gray-50 dark:bg-gray-100 dark:border-gray-600 
                    placeholder-gray-500 h-10 mt-1 mb-6"
          />
          <label className="text-lg mt-8" htmlFor="cnpjinput">
            Cnpj
          </label>
          <input
            type="text"
            id="cnpjinput"
            placeholder="CNPJ..."
            className="block w-full p-4 ps-4 text-sm text-black border border-gray-300 rounded-lg 
                    bg-gray-50 dark:bg-gray-100 dark:border-gray-600 
                    placeholder-gray-500 h-10 mt-1 mb-6"
          />
          <label className="text-lg mt-8" htmlFor="adrressinput">
            Address
          </label>
          <input
            type="text"
            id="adrressinput"
            placeholder="Address..."
            className="block w-full p-4 ps-4 text-sm text-black border border-gray-300 rounded-lg 
                    bg-gray-50 dark:bg-gray-100 dark:border-gray-600 
                    placeholder-gray-500 h-10 my-1"
          />
          <button
            className="font-bold text-lg hover:bg-blue-800 cursor-pointer 
                        hover:transition hover:duration-300 bg-blue-700
                        rounded-md py-2 px-4 text-white w-[100%] mt-4"
          >
            Create Account
          </button>
        </form>
        <div className="flex justify-end mt-2">
          <Link
            to={"/login"}
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
