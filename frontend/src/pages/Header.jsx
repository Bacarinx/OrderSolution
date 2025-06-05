import { PiBellDuotone } from "react-icons/pi";
import { Outlet, Link } from "react-router-dom";

function Header() {
  return (
    <>
      <div>
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
              to="/products"
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
            <div className="hover:bg-gray-200 transition hover:duration-500 rounded-full w-8 h-8 flex items-center justify-center cursor-pointer">
              <PiBellDuotone className="size-6" />
            </div>
            <div className="size-12 bg-gray-500 rounded-full cursor-pointer"></div>
          </div>
        </header>
        <div className="flex justify-center items-center m-6">
          <hr className="w-[100%] text-gray-300" />
        </div>
      </div>
      <Outlet />
    </>
  );
}

export default Header;
