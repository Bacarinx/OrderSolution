/* eslint-disable import/no-unresolved */
import { PiBellDuotone } from "react-icons/pi";
import { Outlet, Link } from "react-router-dom";
import UserMenu from "../components/Header/UserMenu";
import { useUser } from "../contexts/UserContext";

function Header() {
  const { user } = new useUser();

  return (
    <>
      <div className="relative z-0">
        <header className="flex justify-between items-center m-6">
          <div>
            <span className="text-2xl font-bold">OrderSolution</span>
          </div>
          <div className="flex gap-6 items-center">
            <Link
              to="/"
              className="text-xl hover:text-gray-700 hover:underline hover:transition hover:duration-300 cursor-pointer"
            >
              Menu
            </Link>
            <Link
              to="/category"
              className="text-xl hover:text-gray-700 hover:underline hover:transition hover:duration-300 cursor-pointer"
            >
              Products
            </Link>
            <Link
              to="/service"
              className="text-xl hover:text-gray-700 hover:underline hover:transition hover:duration-300 cursor-pointer"
            >
              Service
            </Link>
            <Link
              to="/tabs"
              className="text-xl hover:text-gray-700 hover:underline hover:transition hover:duration-300 cursor-pointer"
            >
              Tabs
            </Link>
            {user ? (
              <UserMenu />
            ) : (
              <Link
                to="/login"
                className="font-bold text-lg hover:bg-blue-800 cursor-pointer 
                            hover:transition hover:duration-300 bg-blue-700 
                            rounded-md py-2 px-4 text-white"
              >
                Login
              </Link>
            )}
          </div>
        </header>
        <div className="flex justify-center items-center m-6 relative">
          <hr className="w-[100%] text-gray-300 relative z-0" />
        </div>
      </div>
      <Outlet />
    </>
  );
}

export default Header;
