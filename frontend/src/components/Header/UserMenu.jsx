/* eslint-disable import/no-unresolved */
/* eslint-disable jsx-a11y/no-static-element-interactions */
/* eslint-disable jsx-a11y/click-events-have-key-events */
import { useState } from "react";
import { FaUserCircle } from "react-icons/fa";
import { useNavigate } from "react-router-dom";
import { useUser } from "../../contexts//UserContext";

function UserMenu() {
  const [open, setOpen] = useState(false);
  const { user, logout } = useUser();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate("/login");
  };

  return (
    <div className="relative inline-block text-left">
      <div
        className="cursor-pointer text-3xl text-blue-700"
        onClick={() => setOpen(!open)}
      >
        <FaUserCircle />
      </div>

      {open && (
        <div
          className="absolute right-0 mt-2 w-48 bg-white border border-gray-300 rounded-lg shadow-lg z-50"
          onMouseLeave={() => setOpen(false)}
        >
          <div className="px-4 py-2 text-gray-700 border-b border-gray-200">
            {user.name || user.email}
          </div>
          <button
            className="block w-full text-left px-4 py-2 hover:bg-gray-100 text-sm"
            onClick={() => {
              navigate("/profile");
              setOpen(false);
            }}
          >
            Ver perfil
          </button>
          <button
            className="block w-full text-left px-4 py-2 hover:bg-gray-100 text-sm text-red-600"
            onClick={handleLogout}
          >
            Sair
          </button>
        </div>
      )}
    </div>
  );
}

export default UserMenu;
