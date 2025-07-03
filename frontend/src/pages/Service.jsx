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
    <div className="relative flex size-full min-h-screen flex-col bg-gray-50 group/design-root overflow-x-hidden">
      <div className="layout-container flex h-full grow flex-col">
        <div className="px-40 flex flex-1 justify-center py-5">
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
              <div className="overflow-hidden rounded-xl border border-[#d4dce2] bg-white shadow-sm">
                <table className="w-full">
                  <thead>
                    <tr className="grid px-4  grid-cols-4 bg-gray-100 text-sm font-semibold text-[#101518]">
                      <th className="py-3 text-center">Data de Abertura</th>
                      <th className="py-3 text-center">Qtde. Clientes</th>
                      <th className="py-3 text-center">Status do Serviço</th>
                      <th className="py-3 text-center">Ação</th>
                    </tr>
                  </thead>
                  <tbody>
                    {openServices.map((os, index) => (
                      <tr
                        key={index}
                        className="grid px-4  grid-cols-4 border-t border-gray-200 text-sm text-[#5c748a] text-center hover:bg-gray-50 transition items-center"
                      >
                        <td className="py-4">
                          {dayjs(os.startService).format("DD/MM/YYYY HH:mm")}
                        </td>
                        <td className="py-4">{os.clients.length}</td>
                        <td className="py-4">
                          <span className="inline-block rounded-full bg-green-100 text-green-800 px-3 py-1 text-xs font-medium">
                            Aberto
                          </span>
                        </td>
                        <td className="py-4">
                          <button
                            onClick={() => navigate(`/service/active`)}
                            className="rounded-md bg-blue-600 hover:bg-blue-700 text-white text-sm px-4 py-2 transition"
                          >
                            Abrir Serviço
                          </button>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            </div>
            <h2 className="text-blue-700 text-[22px] font-bold leading-tight tracking-[-0.015em] px-4 pb-3 pt-5">
              Servicos Antigos
            </h2>
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
          </div>
        </div>
      </div>
    </div>
  );
}

export default Service;
