/* eslint-disable react/prop-types */
import axios from "axios";
import { BsFillTrash3Fill } from "react-icons/bs";
import { useNavigate } from "react-router";
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";
import Cookie from "universal-cookie";

function OrderItems({ comanda, products, onRemoveItem, onSaveTab }) {
  const navigate = useNavigate();
  const MySwal = withReactContent(Swal);

  if (!comanda) return <div>Carregando...</div>;

  const cancelFunction = () => {
    navigate("/service/active");
  };

  const CloseTab = async () => {
    const cookie = new Cookie();
    const jwt = cookie.get("jwt_authorization");

    const result = await MySwal.fire({
      title: "Tem certeza?",
      text: "Deseja realmente Fechar a Comanda?",
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#d33",
      cancelButtonColor: "#3085d6",
      confirmButtonText: "Sim, Fechar",
      cancelButtonText: "Cancelar",
    });

    if (!result.isConfirmed) return;

    try {
      await axios.put(
        `http://localhost:5194/TabProducts/${comanda.tabId}`,
        comanda.tabId,
        {
          headers: {
            Authorization: `Bearer ${jwt}`,
          },
        },
      );

      MySwal.fire("Fechado!", "A comanda foi fechada com sucesso!", "success");
      navigate("/service/active");
    } catch {
      MySwal.fire("Erro!", "Erro ao fechar comanda", "error");
    }
  };

  let value = 0;
  products.map((p) => {
    if (p.isActive) value += p.value;
  });

  return (
    <div className="col-span-1 flex flex-col h-full border rounded-xl p-4 bg-white shadow">
      {/* Lista de itens com rolagem */}
      <div className="flex-1 overflow-y-auto max-h-[calc(100vh-340px)] pr-2">
        <h3 className="text-xl font-semibold mb-3 text-gray-700">
          Itens Atuais
        </h3>

        {products.length === 0 ? (
          <p className="text-gray-500 italic">Não há itens na comanda.</p>
        ) : (
          <ul className="space-y-1">
            {products.map((product, i) => (
              <li
                key={i}
                className={`${product.isActive ? "bg-white" : "bg-gray-300"} flex justify-between items-center border-b border-gray-200 last:border-b-0  
                            rounded-md shadow-sm`}
              >
                <div className="flex flex-col py-1">
                  <span
                    className={`font-medium  px-2 ${product.isActive ? "text-gray-700" : "text-red-700 line-through"}`}
                  >
                    {product.productName}{" "}
                  </span>
                  <span className="font-medium px-2 ">
                    {!product.isActive ? (
                      <>
                        <span className="line-through text-green-600">
                          (R$ {product.value.toFixed(2)})
                        </span>{" "}
                        <span className="font-bold text-green-700">
                          R$ 0,00{" "}
                        </span>
                      </>
                    ) : (
                      <span className="font-bold text-green-700">
                        R$ {product.value.toFixed(2)}{" "}
                      </span>
                    )}
                  </span>
                </div>

                <button
                  onClick={() =>
                    onRemoveItem(product.tabProductId, product.value)
                  }
                  className="p-2 mr-2 bg-red-500 text-white rounded-md hover:bg-red-600 transition duration-150"
                >
                  <BsFillTrash3Fill />
                </button>
              </li>
            ))}
          </ul>
        )}
      </div>

      {/* Rodapé fixo */}
      <div className="mt-4 pt-4 border-t border-gray-300">
        <div className="text-right text-2xl font-bold text-gray-800">
          Total:{" "}
          <span className="text-green-600">
            R$ {value.toFixed(2).replace(".", ",")}
          </span>
        </div>
        <div className="mt-4 flex justify-end gap-3">
          <button
            className="w-full px-5 py-2 bg-red-600 text-white font-semibold rounded-md hover:bg-red-700 transition duration-150"
            onClick={cancelFunction}
          >
            Cancelar
          </button>
          <button
            className="w-full px-5 py-2 bg-green-600 text-white font-semibold rounded-md hover:bg-green-700 transition duration-150"
            onClick={onSaveTab}
          >
            Salvar Comanda
          </button>
          <button
            className="w-full px-5 py-2 bg-yellow-600 text-white font-semibold rounded-md hover:bg-yellow-700 transition duration-150"
            onClick={() => CloseTab()}
          >
            Fechar Comanda
          </button>
        </div>
      </div>
    </div>
  );
}

export default OrderItems;
