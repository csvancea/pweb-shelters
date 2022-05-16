import "./App.css";
import React from "react";
import { BrowserRouter } from "react-router-dom";
import Router from "./utils/Routing";
import "./styling/admin.tailwind.css";
import "./styling/global.tailwind.css";
import Wrapper from "./utils/AuthWrapper";

const App = () => {
  return (
    <Wrapper>
      <BrowserRouter>
        <Router />
      </BrowserRouter>
    </Wrapper>
  );
};

export default App;
