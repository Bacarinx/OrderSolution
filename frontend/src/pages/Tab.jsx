import axios from "axios";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import Cookie from "universal-cookie";

function TabDetails() {
  const { tabcode } = useParams();
  const [tab, setTab] = useState(null);

  const formatDateTime = (dateString) => {
    return new Intl.DateTimeFormat("pt-BR", {
      dateStyle: "short",
      timeStyle: "short",
    }).format(new Date(dateString));
  };

  useEffect(() => {
    const fetchTab = async () => {
      const cookie = new Cookie();
      const jwt = cookie.get("jwt_authorization");

      try {
        const res = await axios.get(`http://localhost:5194/Tab/${tabcode}`, {
          headers: {
            Authorization: `Bearer ${jwt}`,
          },
        });

        setTab(res.data);
      } catch (error) {
        console.error("Erro ao buscar comanda:", error);
      }
    };

    fetchTab();
  }, [tabcode]);

  if (!tab) {
    return (
      <p className="text-center text-gray-500 mt-10">Carregando comanda...</p>
    );
  }

  return (
    <div className="max-w-3xl mx-auto mt-10 p-8 bg-white shadow-md rounded-xl border border-gray-200">
      {/* Cabeçalho */}
      <h1 className="text-3xl font-bold text-blue-700 mb-6">
        Comanda {tab.code}
      </h1>

      {/* Informações da comanda */}
      <div className="grid grid-cols-2 gap-4 mb-6 text-gray-800">
        <div>
          <span className="font-semibold">Código da Comanda:</span> {tab.code}
        </div>
        <div>
          <span className="font-semibold">Nome da Pessoa:</span>{" "}
          {tab.clientName || "Não informado"}
        </div>
        <div>
          <span className="font-semibold">CPF:</span>{" "}
          {tab.clientCPF || "Não informado"}
        </div>
        <div>
          <span className="font-semibold">Status:</span>{" "}
          <span
            className={`inline-block px-2 py-1 text-sm rounded-full font-medium ${
              tab.isOpen
                ? "bg-green-100 text-green-800"
                : "bg-red-100 text-red-800"
            }`}
          >
            {tab.isOpen ? "Aberta" : "Fechada"}
          </span>
        </div>
        <div>
          <span className="font-semibold">Valor Acumulado:</span> R${" "}
          {tab.value?.toFixed(2)}
        </div>
        <div className="col-span-2 mt-2">
          <button className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700 transition">
            Visualizar Pessoa
          </button>
        </div>
      </div>

      {/* Itens da comanda */}
      <div>
        <h2 className="text-xl font-bold mb-3 text-gray-700">
          Itens da Comanda
        </h2>
        {tab.products?.length > 0 ? (
          <table className="w-full table-auto border-collapse text-left text-sm">
            <thead>
              <tr className="bg-gray-100">
                <th className="px-4 py-2 border">Item</th>
                <th className="px-4 py-2 border">Data de Inserção</th>
                <th className="px-4 py-2 border">Valor</th>
              </tr>
            </thead>
            <tbody>
              {tab.products.map((item, index) => (
                <tr key={index} className="border-t">
                  <td className="px-4 py-2">{item.productName}</td>
                  <td className="px-4 py-2">
                    {formatDateTime(item.insertionDate).replace(",", "")}
                  </td>
                  <td className="px-4 py-2">R$ {item.value.toFixed(2)}</td>
                </tr>
              ))}
            </tbody>
          </table>
        ) : (
          <p className="text-gray-500">Nenhum item registrado.</p>
        )}
      </div>
    </div>
  );
}

export default TabDetails;
