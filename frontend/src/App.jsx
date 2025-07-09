/* eslint-disable import/no-unresolved */
import "./index.css";
import { Routes, Route } from "react-router-dom";
import PrivateRouter from "./components/PrivateRouter";
import Categories from "./pages/Categories";
import ClientDetails from "./pages/ClientDetails";
import Clients from "./pages/Clients";
import Home from "./pages/Home";
import Login from "./pages/Login";
import NewCategory from "./pages/NewCategory";
import NewClient from "./pages/NewClient";
import NewProduct from "./pages/NewProduct";
import NewTab from "./pages/NewTab";
import NotFound from "./pages/NotFound";
import Products from "./pages/Products";
import Profile from "./pages/Profile";
import Register from "./pages/Register";
import Service from "./pages/Service";
import ServiceActive from "./pages/ServiceActive";
import ServiceManagement from "./pages/ServiceManagement";
import Tab from "./pages/Tab";
import Tabs from "./pages/Tabs";

function App() {
  return (
    <Routes>
      <Route path="/" element={<Home />} />

      {/* Rotas Protegidas */}
      <Route element={<PrivateRouter />}>
        <Route path="/clients" element={<Clients />} />
        <Route path="/clients/new-client" element={<NewClient />} />
        <Route path="/clients/:id" element={<ClientDetails />} />
        <Route path="/category" element={<Categories />} />
        <Route path="/category/:id" element={<Products />} />
        <Route path="/category/:id/new-product" element={<NewProduct />} />
        <Route path="/category/new" element={<NewCategory />} />
        <Route path="/service" element={<Service />} />
        <Route path="/service/active" element={<ServiceManagement />} />
        <Route path="/service/active/:code" element={<ServiceActive />} />
        <Route path="/tabs" element={<Tabs />} />
        <Route path="/tabs/:tabcode" element={<Tab />} />
        <Route path="/tabs/new" element={<NewTab />} />
        <Route path="/profile" element={<Profile />} />
      </Route>

      <Route path="/login" element={<Login />} />
      <Route path="/register" element={<Register />} />
      <Route path="*" element={<NotFound />} />
    </Routes>
  );
}

export default App;
