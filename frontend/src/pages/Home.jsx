/* eslint-disable jsx-a11y/no-static-element-interactions */
/* eslint-disable jsx-a11y/click-events-have-key-events */
import { useNavigate } from "react-router-dom";

function Home() {
  const navigate = useNavigate();

  return (
    <div className=" flex flex-col items-center justify-center p-6">
      <div className="max-w-4xl w-full bg-white shadow-md rounded-xl p-8">
        <h1 className="text-4xl font-bold text-blue-700 mb-4 text-center">
          Bem-vindo ao Sistema de Comandas
        </h1>
        <p className="text-gray-700 text-lg text-center mb-8">
          Gerencie seus serviços, comandas, clientes e produtos de forma prática
          e eficiente.
        </p>

        <div className="grid grid-cols-1 md:grid-cols-3 gap-6 text-center">
          <div
            onClick={() => navigate("/tabs")}
            className="cursor-pointer border rounded-lg p-6 shadow hover:shadow-lg transition"
          >
            <h2 className="text-xl font-semibold text-blue-600 mb-2">
              Comandas
            </h2>
            <p className="text-gray-600">
              Visualize e gerencie as comandas ativas ou fechadas.
            </p>
          </div>

          <div
            onClick={() => navigate("/category")}
            className="cursor-pointer border rounded-lg p-6 shadow hover:shadow-lg transition"
          >
            <h2 className="text-xl font-semibold text-blue-600 mb-2">
              Produtos
            </h2>
            <p className="text-gray-600">
              Gerencie categorias e produtos disponíveis para venda.
            </p>
          </div>

          <div
            onClick={() => navigate("/service")}
            className="cursor-pointer border rounded-lg p-6 shadow hover:shadow-lg transition"
          >
            <h2 className="text-xl font-semibold text-blue-600 mb-2">
              Serviço Atual
            </h2>
            <p className="text-gray-600">
              Acesse o serviço em andamento e acompanhe clientes e consumo.
            </p>
          </div>
        </div>

        <div className="mt-10 text-center">
          <p className="text-gray-500 text-sm">
            Desenvolvido para facilitar o controle de consumo em restaurantes e
            bares.
          </p>
        </div>
      </div>
    </div>
  );
}

export default Home;
