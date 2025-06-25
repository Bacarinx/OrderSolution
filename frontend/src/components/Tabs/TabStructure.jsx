/* eslint-disable react/prop-types */
import { Link } from "react-router";

function TabStructure({ code, clientName, clientCPF, value }) {
  return (
    <div className="rounded-4xl w-full border-zinc-600 border p-4 my-6">
      <h1 className="my-6 text-4xl font-bold text-center">{code}</h1>
      <div className="mt-2">
        <span className="font-bold">Cliente: </span>
        <span>{clientName}</span>
      </div>
      <div className="mt-2">
        <span className="font-bold">CPF: </span>
        <span>{clientCPF}</span>
      </div>
      <div className="mt-2">
        <span className="font-bold">Valor: </span>
        <span>R${value}</span>
      </div>
      <Link className="" to={`/tabs/${code}`}>
        <div className=" rounded-xl w-full h-5 bg-blue-700 text-white hover:bg-blue-500 flex justify-center items-center font-bold p-4 mt-4">
          Visualizar comanda
        </div>
      </Link>
    </div>
  );
}

export default TabStructure;
