/* eslint-disable react-hooks/exhaustive-deps */
import axios from "axios";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import Cookie from "universal-cookie";

function ServiceManagement() {
  const [service, setService] = useState([]);
  const [value, setValue] = useState(0);
  const navigate = useNavigate();
  const cookie = new Cookie();
  const jwt = cookie.get("jwt_authorization");
  const [code, setCode] = useState("");
  const [tabs, setTabs] = useState(0);
  const [clients, setClients] = useState(0);
  const [errors, setErrors] = useState([]);
  const [tabErrors, setTabErrors] = useState(null);

  const formatToBrazilTime = (utcString) => {
    const fixedUtc = utcString.endsWith("Z") ? utcString : utcString + "Z";
    const date = new Date(fixedUtc);
    return date.toLocaleString("pt-BR", {
      timeZone: "America/Sao_Paulo",
      hour12: false,
    });
  };

  const fetchService = async () => {
    try {
      const res = await axios.get("http://localhost:5194/Service", {
        headers: { Authorization: `Bearer ${jwt}` },
        data: {},
      });

      const openService = res.data.filter((s) => s.endService == null);

      if (openService.length == 0) {
        setErrors([...errors, "Não há serviços abertos!"]);
        return;
      }
      setService(openService);
      setValue(openService[0].value);
      setClients(fetchClients(openService[0].serviceId));
    } catch {
      setErrors([...errors, "Erro ao carregar serviço"]);
    }
  };

  const fetchTabs = async () => {
    try {
      let res = await axios.get("http://localhost:5194/Tab", {
        headers: { Authorization: `Bearer ${jwt}` },
        data: { pagenumber: 0 },
      });
      res = res.data;
      const tab = res.tabs.filter((s) => s.isOpen);
      setTabs(tab.length);
    } catch {
      setErrors([...errors, "Erro ao carregar serviço"]);
    }
  };

  const fetchClients = async (serviceId) => {
    try {
      const res = await axios.get(
        `http://localhost:5194/ServiceClients/${serviceId}`,
        {
          headers: { Authorization: `Bearer ${jwt}` },
        },
      );

      const serviceClients = res.data;
      return serviceClients.clients.length;
    } catch {
      setErrors([...errors, "Erro ao carregar serviço"]);
    }
  };

  useEffect(() => {
    fetchService();
    fetchTabs();
  }, []);

  const handleSearchTab = async (e) => {
    e.preventDefault();
    const cookie = new Cookie();
    const jwt = cookie.get("jwt_authorization");

    try {
      const res = await axios.get(`http://localhost:5194/Tab/${code}`, {
        headers: {
          Authorization: `Bearer ${jwt}`,
        },
      });

      setTabErrors(null);
      if (res.data) {
        navigate(`${res.data.code}`);
      }
    } catch {
      setTabErrors(["Não foi possivel encontrar uma comanda."]);
    }
  };

  return (
    <>
      <div className="flex flex-col items-center">
        <div className="w-[70%]">
          {errors && (
            <div className="text-center mt-6">
              <span className="text-2xl font-bold text-blue-700 ">
                {errors}
              </span>
            </div>
          )}
          {errors.length == 0 && (
            <>
              <h1 className="text-blue-700 font-bold text-3xl mt-4">
                Busque por uma comanda para Gerenciá-la
              </h1>

              {/* Painel com resumo do serviço */}
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4 mt-6 mb-6">
                <div className="bg-white p-4 rounded shadow">
                  <h2 className="font-bold text-gray-700 mb-2">
                    Abertura do Serviço
                  </h2>
                  <p className="text-md">
                    {service.length > 0 &&
                      formatToBrazilTime(service[0].startService).replace(
                        ",",
                        "",
                      )}
                  </p>
                </div>
                <div className="bg-white p-4 rounded shadow">
                  <h2 className="font-bold text-gray-700">Comandas Abertas</h2>
                  <p className="text-xl text-blue-700">{tabs}</p>
                </div>
                <div className="bg-white p-4 rounded shadow">
                  <h2 className="font-bold text-gray-700">Total Consumido</h2>
                  <p className="text-xl text-green-600">
                    R${value.toFixed(2).replace(".", ",")}
                  </p>
                </div>
                <div className="bg-white p-4 rounded shadow">
                  <h2 className="font-bold text-gray-700">
                    Clientes no Serviço
                  </h2>
                  <p className="text-xl text-gray-800">{clients}</p>
                </div>
              </div>

              {/* Busca por código de comanda */}
              <form
                className="max-w-md my-4"
                onSubmit={(e) => {
                  handleSearchTab(e);
                }}
              >
                <label
                  htmlFor="default-search"
                  className="mb-2 text-sm font-medium text-gray-900 sr-only dark:text-white"
                >
                  Buscar comanda
                </label>
                <div className="relative">
                  <div className="absolute inset-y-0 start-0 flex items-center ps-3 pointer-events-none">
                    <svg
                      className="w-4 h-4 text-gray-500"
                      aria-hidden="true"
                      xmlns="http://www.w3.org/2000/svg"
                      fill="none"
                      viewBox="0 0 20 20"
                    >
                      <path
                        stroke="currentColor"
                        strokeLinecap="round"
                        strokeLinejoin="round"
                        strokeWidth="2"
                        d="m19 19-4-4m0-7A7 7 0 1 1 1 8a7 7 0 0 1 14 0Z"
                      />
                    </svg>
                  </div>
                  <input
                    type="search"
                    id="default-search"
                    onChange={(e) => {
                      setCode(e.target.value);
                    }}
                    className="block w-full p-4 ps-10 text-sm text-gray-900 border border-gray-300 rounded-lg bg-gray-50 focus:ring-blue-500 focus:border-blue-500"
                    placeholder="Código da comanda"
                    required
                  />
                </div>
              </form>
              {tabErrors && (
                <div className="text-blue-700 font-bold">
                  Não foi encontrada nenhuma comanda com esse código, tente
                  novamente!
                </div>
              )}
            </>
          )}
        </div>
      </div>
    </>
  );
}

export default ServiceManagement;
