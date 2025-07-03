/* eslint-disable react/prop-types */
import { BsFillTrash3Fill } from "react-icons/bs";

function OrderItems({ comanda, onRemoveItem, onSaveTab }) {
  if (!comanda) return <div>Carregando...</div>;

  return (
    <div className="col-span-1 flex flex-col h-full border rounded-xl p-4 bg-white shadow">
      {/* Lista de itens com rolagem */}
      <div className="flex-1 overflow-y-auto max-h-[calc(100vh-340px)] pr-2">
        <h3 className="text-xl font-semibold mb-3 text-gray-700">
          Itens Atuais
        </h3>

        {comanda.products.length === 0 ? (
          <p className="text-gray-500 italic">Não há itens na comanda.</p>
        ) : (
          <ul className="space-y-1">
            {comanda.products.map((product) => (
              <li
                key={product.id}
                className="flex justify-between items-center border-b border-gray-200 last:border-b-0 bg-white rounded-md shadow-sm"
              >
                <div className="flex flex-col py-1">
                  <span className="font-medium text-gray-700 px-2">
                    {product.productName}{" "}
                  </span>
                  <span className="font-medium px-2 text-green-600">
                    R$ {product.value.toFixed(2)}
                  </span>
                </div>

                <button
                  onClick={() => onRemoveItem(product.tabProductId)}
                  className="px-3 py-3 bg-red-500 text-white rounded-md hover:bg-red-600 transition duration-150"
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
            R$ {comanda.value?.toFixed(2).replace(".", ",")}
          </span>
        </div>
        <div className="mt-4 flex justify-end gap-3">
          <button
            className="w-full px-5 py-2 bg-red-600 text-white font-semibold rounded-md hover:bg-red-700 transition duration-150"
            onClick={onSaveTab}
          >
            Cancelar
          </button>
          <button
            className="w-full px-5 py-2 bg-green-600 text-white font-semibold rounded-md hover:bg-green-700 transition duration-150"
            onClick={onSaveTab}
          >
            Salvar Comanda
          </button>
        </div>
      </div>
    </div>
  );
}

export default OrderItems;
