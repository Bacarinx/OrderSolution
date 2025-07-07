import axios from "axios";
import { useEffect, useState } from "react";
import { Link } from "react-router";
import Cookie from "universal-cookie";

function Categories() {
  const [categories, setCategories] = useState([]);

  useEffect(() => {
    const fetchCategories = async () => {
      const cookie = new Cookie();
      const jwt = cookie.get("jwt_authorization");

      try {
        const res = await axios.get("http://localhost:5194/Category", {
          headers: {
            Authorization: `Bearer ${jwt}`,
          },
        });
        setCategories(res.data.categories);
      } catch (err) {
        console.error("Erro ao buscar categorias:", err);
      }
    };

    fetchCategories();
  }, []);

  return (
    <div className="max-w-4xl mx-auto mt-10 p-6 bg-white shadow rounded-xl">
      <div className="flex justify-between items-center">
        <h1 className="text-3xl font-bold text-blue-700 mb-6">
          Selecione uma Categoria
        </h1>
        <button className="bg-blue-700 p-3 rounded-md font-bold text-white hover:bg-blue-600">
          <Link to={"/category/new"}>New Category</Link>
        </button>
      </div>

      <div className="flex flex-wrap gap-4 mb-6">
        {categories.length > 0 ? (
          categories.map((cat, index) => (
            <Link to={`/category/${cat.categoryName}`} key={index}>
              <button
                className={
                  "px-4 py-2 rounded-full border transition hover:bg-blue-600 hover:text-white hover:font-semibold bg-gray-100 text-gray-800"
                }
              >
                {cat.categoryName}
              </button>
            </Link>
          ))
        ) : (
          <div>Não há categorias existentes</div>
        )}
      </div>
    </div>
  );
}

export default Categories;
