import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import { BrowserRouter } from "react-router";
import App from "./App.jsx";
import { UserProvider } from "./contexts/UserContext.jsx";
// eslint-disable-next-line import/no-unresolved
import Header from "./pages/Header";

createRoot(document.getElementById("root")).render(
  <StrictMode>
    <BrowserRouter>
      <UserProvider>
        <Header />
        <App />
      </UserProvider>
    </BrowserRouter>
  </StrictMode>,
);
