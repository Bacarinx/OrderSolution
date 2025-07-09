/* eslint-disable import/no-unresolved */
import { Navigate, Outlet } from "react-router-dom";
import { useUser } from "../contexts/UserContext";

const PrivateRouter = () => {
  const { user, loading } = useUser();

  if (loading) {
    return <div>Carregando Usuário...</div>;
  }

  console.log(user);
  return user ? <Outlet /> : <Navigate to={"/login"} />;
};

export default PrivateRouter;
