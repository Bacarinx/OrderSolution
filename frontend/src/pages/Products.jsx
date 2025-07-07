import axios from "axios";
import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import Cookie from "universal-cookie";

function Products() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [products, setProducts] = useState([]);
  const [categoryName, setCategoryName] = useState("");

  useEffect(() => {
    const cookie = new Cookie();
    const jwt = cookie.get("jwt_authorization");

    const fetchData = async () => {
      try {
        const productsRes = await axios.get("http://localhost:5194/Product", {
          headers: { Authorization: `Bearer ${jwt}` },
        });

        const categories = await axios.get("http://localhost:5194/Category", {
          headers: { Authorization: `Bearer ${jwt}` },
        });

        const category = categories.data.categories.filter(
          (c) => c.categoryName.toUpperCase() == id.toUpperCase(),
        );
        const categoryId = category[0].categoryId;

        let filteredProducts = productsRes.data.filter(
          (p) => p.categoryId == categoryId,
        );
        setProducts(filteredProducts);
        setCategoryName(id);
      } catch (err) {
        console.error("Erro ao buscar produtos ou categoria:", err);
      }
    };

    fetchData();
  }, [id]);

  const handleDelete = async (productId) => {
    const confirm = window.confirm("Deseja realmente excluir este produto?");
    if (!confirm) return;

    const cookie = new Cookie();
    const jwt = cookie.get("jwt_authorization");

    try {
      await axios.delete(`http://localhost:5194/Product/${productId}`, {
        headers: {
          Authorization: `Bearer ${jwt}`,
        },
      });

      // Atualiza a lista localmente após deletar
      setProducts((prev) => prev.filter((p) => p.id !== productId));
    } catch (err) {
      console.error("Erro ao excluir produto:", err);
      alert("Erro ao excluir produto.");
    }
  };

  return (
    <div className="max-w-4xl mx-auto mt-10 p-6 bg-white shadow rounded-xl">
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-3xl font-bold text-blue-700">
          <strong>Categoria:</strong> {categoryName}
        </h1>
        <div className="flex gap-2">
          <button
            onClick={() => navigate(`/category/${id}/new-product`)}
            className="bg-green-600 text-white px-4 py-2 rounded hover:bg-green-700 transition"
          >
            Criar Produto
          </button>
          <button
            onClick={() =>
              navigate(`/category Bacarin54
              `)
            }
            className="bg-red-600 text-white px-4 py-2 rounded hover:bg-red-700 transition"
          >
            Cancelar
          </button>
        </div>
      </div>

      {products.length > 0 ? (
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          {products.map((p, index) => (
            <div
              key={index}
              className="border p-4 rounded shadow-sm hover:shadow-md transition flex justify-between items-center"
            >
              <div>
                <h3 className="font-semibold text-lg">{p.name}</h3>
                <p className="text-gray-600">
                  Preço: R$ {p.price.toFixed(2).replace(".", ",")}
                </p>
              </div>
              <button
                onClick={() => handleDelete(p.id)}
                className="text-red-600 hover:text-red-800 font-medium"
              >
                Excluir
              </button>
            </div>
          ))}
        </div>
      ) : (
        <p className="text-gray-500">Nenhum produto nesta categoria.</p>
      )}
    </div>
  );
}

export default Products;
