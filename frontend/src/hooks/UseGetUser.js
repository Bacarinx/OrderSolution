import axios from "axios";
import cookies from "universal-cookie";

async function CreateNewUser(data) {
  const res = await axios.post("http://localhost:5194/Register", data, {
    timeout: 10000, // opcional
  });
  return res.data;
}

async function LoginUser(data) {
  const res = await axios.post("http://localhost:5194/Login", data, {
    timeout: 10000, // opcional
  });
  return res.data;
}

async function GetUser() {
  const cookie = new cookies();
  const token = cookie.get("jwt_authorization");

  const res = await axios.get("http://localhost:5194/User", {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });
  return res.data;
}

export { CreateNewUser, LoginUser, GetUser };
