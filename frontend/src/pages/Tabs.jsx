import { useState } from "react";
// eslint-disable-next-line import/no-unresolved
import TabSearchBtn from "../components/Tabs/TabSearchBtn";

function Tabs() {
  const [active, setActive] = useState("all");
  const buttons = ["All", "Closed", "Open"];

  return (
    <div className="flex justify-center flex-col items-center">
      <div className="w-[70%] px-6">
        <div className=" flex items-center justify-between">
          <h1 className="text-5xl font-bold">Tabs</h1>
          <button className="bg-blue-700 hover:bg-blue-800 focus:ring-4 text-white px-4 py-2 rounded-md">
            New Tab
          </button>
        </div>
        <TabSearchBtn />
        <div className="flex gap-6 mb-2">
          {buttons.map((b) => (
            <button
              key={b}
              onClick={() => setActive(b)}
              className={`${active === b ? "font-bold" : "font-normal"}`}
            >
              {b}
            </button>
          ))}
        </div>
        <hr className="text-gray-300" />
      </div>
    </div>
  );
}

export default Tabs;
