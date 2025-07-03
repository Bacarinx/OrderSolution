/* eslint-disable react-hooks/exhaustive-deps */
/* eslint-disable import/no-unresolved */
import axios from "axios";
import { useEffect, useState } from "react";
import { useParams } from "react-router";
import Cookie from "universal-cookie";
import OrderItems from "../components/Services/Active/OrderItems";

function ServiceActive() {
  const { code } = useParams();
  const [tab, setTab] = useState(null);

  const saveItems = () => {};
  const removeItems = () => {};
  const fetchTab = async () => {
    const cookie = new Cookie();
    const jwt = cookie.get("jwt_authorization");

    try {
      const res = await axios.get(`http://localhost:5194/Tab/${code}`, {
        headers: {
          Authorization: `Bearer ${jwt}`,
        },
      });
      setTab(res.data);
      console.log(res.data);
    } catch (e) {
      console.log(e);
    }
  };
  useEffect(() => {
    fetchTab();
  }, []);

  return (
    <div className="px-6">
      {!tab && <div>teste</div>}
      {tab && (
        <div className="">
          <h2 className="text-3xl font-bold mb-4 text-black-600">
            Comanda <span className="text-blue-700">{tab.code}</span>
          </h2>
          <div className="h-[calc(100vh-170px)] grid grid-cols-4 gap-4">
            <OrderItems
              comanda={tab}
              key={code}
              onSaveTab={saveItems}
              onRemoveItem={removeItems}
            />
          </div>
        </div>
      )}
    </div>
  );
}
export default ServiceActive;
