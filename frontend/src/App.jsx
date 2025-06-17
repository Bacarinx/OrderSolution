/* eslint-disable import/no-unresolved */
import "./index.css";
import { Routes, Route } from "react-router-dom";
import Home from "./pages/Home";
import Login from "./pages/Login";
import NotFound from "./pages/NotFound";
import Products from "./pages/Products";
import Register from "./pages/Register";
import Service from "./pages/Service";
import Tabs from "./pages/Tabs";

function App() {
  return (
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/products" element={<Products />} />
      <Route path="/service" element={<Service />} />
      <Route path="/tabs" element={<Tabs />} />
      <Route path="/login" element={<Login />} />
      <Route path="/register" element={<Register />} />
      <Route path="*" element={<NotFound />} />
    </Routes>
  );
}

export default App;
