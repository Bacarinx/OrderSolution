/* eslint-disable react-hooks/exhaustive-deps */
/* eslint-disable import/no-unresolved */
import axios from "axios";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router";
import Cookie from "universal-cookie";
import AddProductMenu from "../components/Services/Active/AddProductMenu";
import OrderItems from "../components/Services/Active/OrderItems";

function ServiceActive() {
  const cookie = new Cookie();
  const jwt = cookie.get("jwt_authorization");

  const { code } = useParams();
  const [openService, setOpenService] = useState();
  const [tab, setTab] = useState(null);
  const [products, setProducts] = useState([]);
  const [categories, setCategories] = useState([]);

  const [listToPost, SetListToPost] = useState([]);
  const navigate = useNavigate();

  const fetchOpenService = async () => {
    const res = await axios.get("http://localhost:5194/Service", {
      headers: { Authorization: `Bearer ${jwt}` },
      data: {},
    });

    const openService = res.data.filter((s) => s.endService == null);

    setOpenService(openService[0].serviceId);
  };

  const saveItems = async () => {
    try {
      await axios.post("http://localhost:5194/TabProducts", listToPost, {
        headers: {
          Authorization: `Bearer ${jwt}`,
        },
      });

      navigate("/service/active");
      // redireciona de volta para a lista da categoria
    } catch (err) {
      console.error("Erro ao criar produto:", err);
    }
  };

  const addProducts = (id, name, price) => {
    SetListToPost([
      ...listToPost,
      {
        tabId: tab.tabId,
        productId: id,
        serviceId: openService,
      },
    ]);

    setProducts([
      ...products,
      {
        tabProductId: null,
        isActive: true,
        productName: name,
        insertionDate: null,
        value: price,
      },
    ]);
  };

  const fetchTab = async () => {
    try {
      const res = await axios.get(`http://localhost:5194/Tab/${code}`, {
        headers: {
          Authorization: `Bearer ${jwt}`,
        },
      });
      setTab(res.data);
      setProducts(res.data.products);
    } catch (e) {
      console.log(e);
    }
  };

  const fetchCategories = async () => {
    const cookie = new Cookie();
    const jwt = cookie.get("jwt_authorization");

    try {
      const res = await axios.get("http://localhost:5194/Category", {
        headers: {
          Authorization: `Bearer ${jwt}`,
        },
      });
      setCategories(res.data.categories);
    } catch (err) {
      console.error("Erro ao buscar categorias:", err);
    }
  };
  useEffect(() => {
    fetchTab();
    fetchCategories();
    fetchOpenService();
  }, []);

  const removeItems = async (id, value) => {
    await axios.patch(
      `http://localhost:5194/TabProducts?idProductTab=${id}`,
      {
        idProductTab: id,
      },
      {
        headers: {
          Authorization: `Bearer ${jwt}`, // cabeçalhos
        },
      },
    );

    setTab((prev) => ({
      ...prev,
      value: prev.value - value,
    }));

    setProducts((prev) =>
      prev.map((p) => {
        return p.tabProductId === id ? { ...p, isActive: false } : p;
      }),
    );
  };

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
              key={code}
              comanda={tab}
              products={products}
              onSaveTab={saveItems}
              onRemoveItem={removeItems}
            />
            <AddProductMenu
              key={code}
              categorias={categories}
              addProducts={addProducts}
            />
          </div>
        </div>
      )}
    </div>
  );
}
export default ServiceActive;
