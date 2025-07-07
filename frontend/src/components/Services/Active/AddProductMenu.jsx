/* eslint-disable jsx-a11y/no-static-element-interactions */
/* eslint-disable jsx-a11y/click-events-have-key-events */
import axios from "axios";
import { useState } from "react";
import Cookies from "universal-cookie";

/* eslint-disable react/prop-types */
function AddProductMenu({ categorias, addProducts }) {
  const [products, setProducts] = useState([]);
  const cookie = new Cookies();
  const jwt = cookie.get("jwt_authorization");

  const fetchProducts = async (categoriaId) => {
    try {
      const productsRes = await axios.get("http://localhost:5194/Product", {
        headers: { Authorization: `Bearer ${jwt}` },
      });

      let filteredProducts = productsRes.data.filter(
        (p) => p.categoryId == categoriaId,
      );
      setProducts(filteredProducts);
      console.log(filteredProducts);
    } catch (err) {
      console.error("Erro ao buscar produtos ou categoria:", err);
    }
  };

  return (
    <div className="col-span-3">
      {products.length == 0 && (
        <div>
          <div className="grid grid-cols-4 gap-2">
            {categorias.map((c, i) => {
              return (
                <div
                  className="w-full border-1 border-blue-600 rounded-lg h-[100px] flex font-bold text-xl
                        justify-center items-center bg-blue-600 hover:bg-blue-500 text-white"
                  key={i}
                  onClick={() => fetchProducts(c.categoryId)}
                >
                  <button>{c.categoryName}</button>
                </div>
              );
            })}
          </div>
        </div>
      )}
      {products.length > 0 && (
        <div>
          <div className="grid grid-cols-4 gap-2">
            {products.map((p, i) => {
              return (
                <div
                  key={i}
                  onClick={() => addProducts(p.id, p.name, p.price)}
                  className="w-full border-1 border-gray-600 rounded-lg h-[100px] flex font-bold text-xl
                        justify-center px-4 bg-gray-200 hover:bg-gray-300 gap-3 text-black flex-col"
                >
                  <span>{p.name}</span>
                  <span className="text-green-600 text-md font-bold">
                    R$ {p.price.toFixed(2).replace(".", ",")}
                  </span>
                </div>
              );
            })}
          </div>
          <button
            onClick={() => setProducts([])}
            className="bg-red-500 text-white rounded-md w-30 h-10 font-bold mt-4"
          >
            Voltar
          </button>
        </div>
      )}
    </div>
  );
}

export default AddProductMenu;
