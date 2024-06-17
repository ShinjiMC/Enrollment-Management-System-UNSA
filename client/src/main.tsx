import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App.tsx";
import "./sass/index.scss";
import { Provider } from "react-redux";
import store from "./store/ReduxStore.ts";
import { BrowserRouter, Route, Routes } from "react-router-dom";

const Main = () => {
  return (
    <React.StrictMode>
      <Provider store={store}>
        <BrowserRouter>
          <Routes>
            <Route path="*" element={<App />} />
          </Routes>
        </BrowserRouter>
      </Provider>
    </React.StrictMode>
  );
};

ReactDOM.createRoot(document.getElementById("root")!).render(<Main />);

export default Main;
