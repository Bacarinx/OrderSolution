import { jwtDecode } from "jwt-decode";
import { createContext, useContext, useEffect, useState } from "react";
import Cookies from "universal-cookie";
import { GetUser } from "../hooks/UseGetUser";

const UserContext = createContext();

// eslint-disable-next-line react/prop-types
export function UserProvider({ children }) {
  const [user, setUser] = useState(null);
  const fetchUser = async () => {
    try {
      const data = await GetUser();
      setUser(data);
    } catch {
      console.log("Erro ao buscar usuário");
    }
  };

  useEffect(() => {
    fetchUser();
  }, []);

  const login = (token) => {
    const decoded = jwtDecode(token);
    const cookie = new Cookies();
    cookie.set("jwt_authorization", token, {
      expires: new Date(decoded.exp * 1000),
    });
    fetchUser();
  };

  const logout = () => {
    const cookie = new Cookies();
    cookie.remove("jwt_authorization");
    setUser(null);
  };

  return (
    <UserContext.Provider value={{ user, login, logout }}>
      {children}
    </UserContext.Provider>
  );
}

export function useUser() {
  return useContext(UserContext);
}
