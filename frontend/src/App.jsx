/* eslint-disable import/no-unresolved */
import "./index.css";
import { Routes, Route } from "react-router-dom";
import Categories from "./pages/Categories";
import Home from "./pages/Home";
import Login from "./pages/Login";
import NewCategory from "./pages/NewCategory";
import NewProduct from "./pages/NewProduct";
import NewTab from "./pages/NewTab";
import NotFound from "./pages/NotFound";
import Products from "./pages/Products";
import Register from "./pages/Register";
import Service from "./pages/Service";
import Tab from "./pages/Tab";
import Tabs from "./pages/Tabs";

function App() {
  return (
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/category" element={<Categories />} />
      <Route path="/category/:id" element={<Products />} />
      <Route path="/category/:id/new-product" element={<NewProduct />} />
      <Route path="/category/new" element={<NewCategory />} />
      <Route path="/service" element={<Service />} />
      <Route path="/tabs" element={<Tabs />} />
      <Route path="/tabs/:tabcode" element={<Tab />} />
      <Route path="/login" element={<Login />} />
      <Route path="/register" element={<Register />} />
      <Route path="*" element={<NotFound />} />
      <Route path="/tabs/new" element={<NewTab />} />
    </Routes>
  );
}

export default App;
