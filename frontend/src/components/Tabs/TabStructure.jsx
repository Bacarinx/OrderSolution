/* eslint-disable react/prop-types */
import { Link } from "react-router";

function TabStructure({ code }) {
  return (
    <div className="rounded-xl w-full border-zinc-600 border p-4 my-4">
      <h1 className="my-2 text-xl font-bold text-center">{code}</h1>
      <Link className="" to={`/tabs/${code}`}>
        <div className=" rounded-xl w-full h-5 bg-blue-700 text-white hover:bg-blue-500 flex justify-center items-center font-bold p-4 mt-2">
          View
        </div>
      </Link>
    </div>
  );
}

export default TabStructure;
