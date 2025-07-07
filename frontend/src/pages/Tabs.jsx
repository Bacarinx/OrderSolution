/* eslint-disable import/no-unresolved */
import axios from "axios";
import { useEffect, useState } from "react";
import { Link } from "react-router";
import Cookie from "universal-cookie";
import TabPagination from "../components/Tabs/TabPagination";
import TabStructure from "../components/Tabs/TabStructure";

function Tabs() {
  const [active, setActive] = useState("All");
  const buttons = ["All", "Closed", "Open"];
  const [tabs, setTabs] = useState({ tabs: [], qtd: 0 });
  const [numberPage, setNumberPage] = useState(1);
  const [code, setCode] = useState("");

  useEffect(() => {
    const fetchTabs = async () => {
      const cookie = new Cookie();
      const jwt = cookie.get("jwt_authorization");
      const res = await axios.get("http://localhost:5194/Tab", {
        headers: {
          Authorization: `Bearer ${jwt}`,
        },
        params: {
          pagenumber: numberPage,
          code: code,
        },
      });
      const allTabs = res.data.tabs;
      const total = res.data.qtd;

      if (active === "All") {
        setTabs({ tabs: allTabs || [], qtd: total });
      } else if (active === "Closed") {
        const result = allTabs ? allTabs.filter((i) => i.isOpen === false) : [];
        setTabs({ tabs: result, qtd: result.length });
      } else if (active === "Open") {
        const result = allTabs ? allTabs.filter((i) => i.isOpen === true) : [];
        setTabs({ tabs: result, qtd: result.length });
      }
    };
    fetchTabs();
  }, [active, code, numberPage]);

  return (
    <div className="flex justify-center flex-col items-center">
      <div className="w-[70%] px-6 mb-8">
        <div className=" flex items-center justify-between">
          <h1 className="text-5xl font-bold">Tabs</h1>
          <button className="bg-blue-700 hover:bg-blue-800 focus:ring-4 text-white px-4 py-2 rounded-md">
            <Link to={"/tabs/new"}>New Tab</Link>
          </button>
        </div>
        <form className="max-w-md my-4">
          <label
            htmlFor="default-search"
            className="mb-2 text-sm font-medium text-gray-900 sr-only dark:text-white"
          >
            Search
          </label>
          <div className="relative">
            <div className="absolute inset-y-0 start-0 flex items-center ps-3 pointer-events-none">
              <svg
                className="w-4 h-4 text-gray-500 dark:text-gray-400"
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
              className="block w-full p-4 ps-10 text-sm text-gray-900 border border-gray-300 rounded-lg bg-gray-50 focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-100 dark:border-gray-600 dark:placeholder-gray-600 dark:text-black dark:focus:ring-blue-500 dark:focus:border-blue-500"
              placeholder="Código da comanda"
              required
            />
          </div>
        </form>
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

        <div className="grid grid-cols-8 gap-4">
          {tabs.tabs.map((m, i) => (
            <TabStructure key={i} code={m.code} />
          ))}
        </div>

        <TabPagination
          qtd={tabs.qtd}
          actualPage={numberPage}
          onPageChange={(n) => setNumberPage(n)}
        ></TabPagination>
      </div>
    </div>
  );
}

export default Tabs;
