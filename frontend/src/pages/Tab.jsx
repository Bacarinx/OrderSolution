import { Combobox } from "@headlessui/react";
import { CheckIcon, ChevronUpDownIcon } from "@heroicons/react/20/solid";
import axios from "axios";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import Cookie from "universal-cookie";

function TabDetails() {
  const { tabcode } = useParams();
  const [tab, setTab] = useState(null);
  const [clients, setClients] = useState([]);
  const [clientToChangeTab, setClientToChangeTab] = useState(null);
  const [query, setQuery] = useState("");

  const navigate = useNavigate();

  const formatDateTime = (dateString) => {
    return new Intl.DateTimeFormat("pt-BR", {
      dateStyle: "short",
      timeStyle: "short",
    }).format(new Date(dateString));
  };

  const cookie = new Cookie();
  const jwt = cookie.get("jwt_authorization");

  const ChangePersonFromTab = async (clientId) => {
    try {
      await axios.patch(
        `http://localhost:5194/Tab/${tab.tabId}`,
        { clientId },
        {
          headers: {
            Authorization: `Bearer ${jwt}`,
          },
        },
      );
      window.location.reload();
    } catch {
      console.error("Erro ocorreu durante a troca de cliente");
    }
  };

  useEffect(() => {
    const fetchTab = async () => {
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

    const fetchClients = async () => {
      try {
        const res = await axios.get("http://localhost:5194/Client", {
          headers: {
            Authorization: `Bearer ${jwt}`,
          },
        });

        setClients(res.data);
      } catch (error) {
        console.error("Erro ao buscar clientes:", error);
      }
    };

    fetchTab();
    fetchClients();
  }, [tabcode, jwt]);

  const filteredClients =
    query === ""
      ? clients
      : clients.filter(
          (client) =>
            client.name.toLowerCase().includes(query.toLowerCase()) ||
            client.cpf?.toLowerCase().includes(query.toLowerCase()),
        );

  if (!tab) {
    return (
      <p className="text-center text-gray-500 mt-10">Carregando comanda...</p>
    );
  }

  return (
    <div className="max-w-3xl mx-auto mt-10 p-8 bg-white shadow-md rounded-xl border border-gray-200">
      <h1 className="text-3xl font-bold text-blue-700 mb-6">
        Comanda {tab.code}
      </h1>

      <div className="grid grid-cols-2 gap-4 mb-6 text-gray-800">
        <div>
          <span className="font-semibold">Código da Comanda:</span> {tab.code}
        </div>
        <div>
          <span className="font-semibold">Nome da Pessoa:</span>{" "}
          {tab.clientName || "-"}
        </div>
        <div>
          <span className="font-semibold">CPF:</span> {tab.clientCPF || "-"}
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
          {!tab.clientId && (
            <div className="flex gap-2">
              <button
                className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700 transition w-fit"
                onClick={() => ChangePersonFromTab(clientToChangeTab)}
              >
                Adicionar Pessoa
              </button>

              {/* Combobox de seleção de cliente */}
              <Combobox
                value={clientToChangeTab}
                onChange={(selectedId) => {
                  setClientToChangeTab(selectedId);
                }}
              >
                <div className="relative mt-1 w-full max-w-md">
                  <div className="relative w-full cursor-default overflow-hidden rounded-lg bg-white text-left shadow-md focus:outline-none sm:text-sm border border-gray-300">
                    <Combobox.Input
                      className="w-full border-none py-2 pl-3 pr-10 text-sm leading-5 text-gray-900 focus:ring-0"
                      displayValue={(selectedId) => {
                        const selected = clients.find(
                          (c) => c.id === selectedId,
                        );
                        return selected
                          ? `${selected.name} - ${selected.cpf}`
                          : "";
                      }}
                      onChange={(event) => setQuery(event.target.value)}
                      placeholder="Buscar cliente por nome ou CPF..."
                    />
                    <Combobox.Button className="absolute inset-y-0 right-0 flex items-center pr-2">
                      <ChevronUpDownIcon
                        className="h-5 w-5 text-gray-400"
                        aria-hidden="true"
                      />
                    </Combobox.Button>
                  </div>

                  {filteredClients.length > 0 && (
                    <Combobox.Options className="absolute z-10 mt-1 max-h-60 w-full overflow-auto rounded-md bg-white py-1 text-base shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none sm:text-sm">
                      {filteredClients.map((client) => (
                        <Combobox.Option
                          key={client.id}
                          className={({ active }) =>
                            `relative cursor-default select-none py-2 pl-10 pr-4 ${
                              active
                                ? "bg-blue-600 text-white"
                                : "text-gray-900"
                            }`
                          }
                          value={client.id}
                        >
                          {({ selected, active }) => (
                            <>
                              <span
                                className={`block truncate ${
                                  selected ? "font-medium" : "font-normal"
                                }`}
                              >
                                {client.name} - {client.cpf}
                              </span>
                              {selected && (
                                <span
                                  className={`absolute inset-y-0 left-0 flex items-center pl-3 ${
                                    active ? "text-white" : "text-blue-600"
                                  }`}
                                >
                                  <CheckIcon
                                    className="h-5 w-5"
                                    aria-hidden="true"
                                  />
                                </span>
                              )}
                            </>
                          )}
                        </Combobox.Option>
                      ))}
                    </Combobox.Options>
                  )}
                </div>
              </Combobox>
            </div>
          )}
          {tab.clientId && (
            <div className="flex gap-2">
              <button
                className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700 transition"
                onClick={() => navigate(`/clients/${tab.clientId}`)}
              >
                Visualizar Pessoa
              </button>
              <button
                className="bg-red-600 text-white px-4 py-2 rounded hover:bg-red-700 transition"
                onClick={() => ChangePersonFromTab(0)}
              >
                Remover Pessoa
              </button>
            </div>
          )}
        </div>
      </div>

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
                <tr
                  key={index}
                  className={`border-t ${!item.isActive ? "bg-red-200" : ""}`}
                >
                  <td
                    className={`px-4 py-2 ${!item.isActive ? "text-red-600 line-through" : ""}`}
                  >
                    {item.productName}
                  </td>
                  <td
                    className={`px-4 py-2 ${!item.isActive ? "text-red-600 line-through" : ""}`}
                  >
                    {formatDateTime(item.insertionDate).replace(",", "")}
                  </td>
                  <td
                    className={`px-4 py-2 ${!item.isActive ? "text-red-600 line-through" : ""}`}
                  >
                    R$ {item.value.toFixed(2)}{" "}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        ) : (
          <p className="text-gray-500">Nenhum item registrado.</p>
        )}
        <button
          className="bg-red-600 mt-4 text-white px-4 py-2 rounded hover:bg-red-700 transition min-w-50"
          onClick={() => navigate(`/tabs`)}
        >
          Voltar
        </button>
      </div>
    </div>
  );
}

export default TabDetails;
