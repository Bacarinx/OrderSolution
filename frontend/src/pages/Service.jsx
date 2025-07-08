import axios from "axios";
import dayjs from "dayjs";
import duration from "dayjs/plugin/duration";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import Cookie from "universal-cookie";
dayjs.extend(duration);

function Service() {
  const getDuration = (start, end) => {
    const diff = dayjs(end).diff(start);
    const dur = dayjs.duration(diff);
    return `${dur.hours()}h ${dur.minutes()}min`;
  };

  const navigate = useNavigate();

  const cookie = new Cookie();
  const jwt = cookie.get("jwt_authorization");

  const [closedServices, setClosedServices] = useState([
    {
      serviceId: null,
      startService: null,
      endService: null,
      clients: [
        {
          clientId: null,
          cpf: null,
          email: null,
          gender: null,
          name: null,
          phoneNumber: null,
        },
      ],
    },
  ]);

  const [openServices, setOpenServices] = useState([
    {
      serviceId: null,
      startService: null,
      endService: null,
      clients: [
        {
          clientId: null,
          cpf: null,
          email: null,
          gender: null,
          name: null,
          phoneNumber: null,
        },
      ],
    },
  ]);

  const startService = async () => {
    try {
      await axios.post(
        `http://localhost:5194/Service`,
        {},
        {
          headers: {
            Authorization: `Bearer ${jwt}`,
          },
        },
      );
      window.location.reload();
    } catch (e) {
      console.log(e);
    }
  };

  const endService = async (serviceId) => {
    try {
      await axios.put(
        `http://localhost:5194/Service/${serviceId}`,
        {},
        {
          headers: {
            Authorization: `Bearer ${jwt}`,
          },
        },
      );

      window.location.reload();
    } catch (e) {
      console.log(e);
    }
  };

  useEffect(() => {
    const fetchServices = async () => {
      const cookie = new Cookie();
      const jwt = cookie.get("jwt_authorization");

      const servicesResponse = await axios.get(
        "http://localhost:5194/Service",
        {
          headers: {
            Authorization: `Bearer ${jwt}`,
          },
        },
      );
      const services = servicesResponse.data;

      setOpenServices(services.filter((a) => a.endService == null));
      setClosedServices(services.filter((a) => a.endService != null));
    };

    fetchServices();
  }, []);

  return (
    <div className="relative flex size-full flex-col  group/design-root overflow-x-hidden">
      <div className="layout-container flex h-full grow flex-col">
        <div className=" flex flex-1 justify-center my-5">
          <div className="layout-content-container flex flex-col max-w-[960px] flex-1">
            <div className="flex flex-wrap justify-between gap-3 p-4">
              <div className="flex min-w-72 flex-col gap-3">
                <p className="text-blue-700 tracking-light text-[32px] font-bold leading-tight">
                  Histórico de Servicos
                </p>
                <p className="text-[#5c748a] text-sm font-normal leading-normal">
                  Reveja servicos antigos, incluindo o histórico de clientes e
                  comandas.
                </p>
              </div>
            </div>
            <h2 className="text-blue-700 text-[22px] font-bold leading-tight tracking-[-0.015em] px-4 pb-3 pt-5">
              Servico Ativo
            </h2>
            <div className="px-4 py-3">
              <div
                className={`overflow-hidden  
                          ${openServices.length > 0 ? "rounded-xl border border-[#d4dce2] bg-white shadow-sm " : ""} `}
              >
                {openServices.length > 0 && (
                  <table className="w-full">
                    <thead>
                      <tr className="grid px-4  grid-cols-5 bg-gray-100 text-sm font-semibold text-[#101518]">
                        <th className="py-3 text-center col-span-1">
                          Data de Abertura
                        </th>
                        <th className="py-3 text-center col-span-1">
                          Qtde. Clientes
                        </th>
                        <th className="py-3 text-center col-span-1">
                          Status do Serviço
                        </th>
                        <th className="py-3 text-center col-span-2">Ação</th>
                      </tr>
                    </thead>
                    <tbody>
                      {openServices.map((os, index) => (
                        <tr
                          key={index}
                          className="grid px-4 grid-cols-5 border-t border-gray-200 text-sm text-[#5c748a] text-center hover:bg-gray-50 transition items-center"
                        >
                          <td className="py-4 col-span-1">
                            {dayjs(os.startService).format("DD/MM/YYYY HH:mm")}
                          </td>
                          <td className="py-4 col-span-1">
                            {os.clients.length}
                          </td>
                          <td className="py-4 col-span-1">
                            <span className="inline-block rounded-full bg-green-100 text-green-800 px-3 py-1 text-xs font-medium">
                              Aberto
                            </span>
                          </td>
                          <td className="py-4 gap-2 flex col-span-1 items-center justify-center">
                            <button
                              onClick={() => navigate(`/service/active`)}
                              className="w-[90%] rounded-md bg-blue-600 hover:bg-blue-700 text-white text-sm px-4 py-2 transition"
                            >
                              Operar Serviço
                            </button>
                          </td>
                          <td className="py-4 gap-2 flex col-span-1 items-center justify-center">
                            <button
                              onClick={() => endService(os.serviceId)}
                              className="w-[90%] rounded-md bg-red-600 hover:bg-red-700 text-white text-sm px-4 py-2 transition"
                            >
                              Fechar Serviço
                            </button>
                          </td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                )}
                {openServices.length == 0 && (
                  <div className="flex flex-col gap-4">
                    <span className="text-gray-700">
                      Não há servicos abertos! Clique no botão abaixo para abrir
                      um servico.
                    </span>
                    <button
                      onClick={() => startService()}
                      className="bg-green-600 text-white rounded-md px-6 py-2 hover:bg-green-700"
                    >
                      Abrir Servico
                    </button>
                  </div>
                )}
              </div>
            </div>
            <h2 className="text-blue-700 text-[22px] font-bold leading-tight tracking-[-0.015em] px-4 pb-3 pt-5">
              Servicos Antigos
            </h2>
            {closedServices.length > 0 && (
              <div className="px-4 py-3">
                <div className="overflow-hidden rounded-xl border border-[#d4dce2] bg-white shadow-sm">
                  <table className="w-full">
                    <thead>
                      <tr className="grid grid-cols-6 bg-gray-100 text-sm font-semibold text-[#101518] px-4 ">
                        <th className="py-3 text-center">Data de Abertura</th>
                        <th className="py-3 text-center">Data de Fechamento</th>
                        <th className="py-3 text-center">Duracao</th>
                        <th className="py-3 text-center">Qtde. Clientes</th>
                        <th className="py-3 text-center">Status do Serviço</th>
                        <th className="py-3 text-center">Ação</th>
                      </tr>
                    </thead>
                    <tbody>
                      {closedServices.map((os, index) => (
                        <tr
                          key={index}
                          className="grid grid-cols-6 px-4 border-t border-gray-200 text-sm text-[#5c748a] text-center hover:bg-gray-50 transition items-center"
                        >
                          <td className="py-4">
                            {dayjs(os.startService).format("DD/MM/YYYY HH:mm")}
                          </td>
                          <td className="py-4">
                            {dayjs(os.endService).format("DD/MM/YYYY HH:mm")}
                          </td>
                          <td className="py-4">
                            {getDuration(os.startService, os.endService)}
                          </td>
                          <td className="py-4">{os.clients.length}</td>
                          <td className="py-4">
                            <span className="inline-block rounded-full bg-red-100 text-red-800 px-3 py-1 text-xs font-medium">
                              Fechado
                            </span>
                          </td>
                          <td className="py-4">
                            <button
                              onClick={() => navigate(`/${os.serviceId}`)}
                              className="rounded-md bg-blue-600 hover:bg-blue-700 text-white text-sm px-4 py-2 transition"
                            >
                              Visualizar Serviço
                            </button>
                          </td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>
              </div>
            )}
            {closedServices.length == 0 && (
              <span className="px-4 text-gray-700">
                Não há servicos Fechados para visualizar!
              </span>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}

export default Service;
