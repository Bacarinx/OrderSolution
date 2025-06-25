/* eslint-disable jsx-a11y/label-has-associated-control */
import axios from "axios";
import { useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import Cookie from "universal-cookie";

function NewProduct() {
  const { id } = useParams(); // <- categoryId
  const navigate = useNavigate();
  const [name, setName] = useState("");
  const [price, setPrice] = useState("");

  const formatToCurrency = (value) => {
    const numeric = value.replace(/[^\d]/g, ""); // remove não numéricos

    if (!numeric) return ""; // <- se estiver vazio, retorna string vazia

    const number = (parseInt(numeric, 10) / 100).toFixed(2);
    return number.replace(".", ",");
  };

  const handlePriceChange = (e) => {
    const value = e.target.value;
    const formatted = formatToCurrency(value);
    setPrice(formatted);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    const cookie = new Cookie();
    const jwt = cookie.get("jwt_authorization");

    const categories = await axios.get("http://localhost:5194/Category", {
      headers: { Authorization: `Bearer ${jwt}` },
    });

    const category = categories.data.categories.filter(
      (c) => c.categoryName.toUpperCase() == id.toUpperCase(),
    );
    const categoryId = category[0].categoryId;

    try {
      await axios.post(
        "http://localhost:5194/Product",
        {
          name,
          price: parseFloat(price),
          categoryId: parseInt(categoryId),
        },
        {
          headers: {
            Authorization: `Bearer ${jwt}`,
          },
        },
      );

      // redireciona de volta para a lista da categoria
      navigate(`/category/${id}`);
    } catch (err) {
      console.error("Erro ao criar produto:", err);
    }
  };

  return (
    <div className="max-w-md mx-auto mt-10 p-6 bg-white rounded shadow">
      <h1 className="text-2xl font-bold mb-4 text-blue-700">Novo Produto</h1>
      <form onSubmit={handleSubmit} className="space-y-4">
        <div>
          <label className="block text-sm font-medium mb-1">
            Nome do Produto
          </label>
          <input
            type="text"
            required
            value={name}
            onChange={(e) => setName(e.target.value)}
            className="w-full px-4 py-2 border rounded-md focus:ring-2 focus:ring-blue-500"
          />
        </div>
        <div>
          <label className="block text-sm font-medium mb-1">Preço</label>
          <div className="relative">
            <span className="absolute inset-y-0 left-3 flex items-center text-gray-500">
              R$
            </span>
            <input
              type="text"
              inputMode="numeric"
              value={price}
              onChange={handlePriceChange}
              className="w-full pl-10 pr-4 py-2 border rounded-md focus:ring-2 focus:ring-blue-500"
              placeholder="0,00"
              required
            />
          </div>
        </div>
        <button
          type="submit"
          className="bg-green-600 text-white px-4 py-2 rounded hover:bg-green-700 transition"
        >
          Criar Produto
        </button>
      </form>
    </div>
  );
}

export default NewProduct;
