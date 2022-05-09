import React, { useEffect } from "react";
import { BrowserRouter, Route, Routes, Navigate } from "react-router-dom";
import Analytics from "../pages/Admin/Analytics";
import Shelter from "../pages/Admin/Shelter";
import UserBooks from "../pages/User/Books";
import Shelters from "../pages/Admin/Shelters";
import Users from "../pages/Admin/Users";
import User from "../pages/Admin/User";
import Account from "../pages/User/Account";
import { useAuth0 } from "@auth0/auth0-react";

const Router = () => {
  const { isAuthenticated, loginWithRedirect } = useAuth0();

  useEffect(() => {
    if (!isAuthenticated) {
      loginWithRedirect();
    }
  }, [isAuthenticated, loginWithRedirect]);

  return (
    isAuthenticated && (
      <BrowserRouter>
        <Routes>
          <Route exact path="/" element={<Navigate to="/shelters" />} />
          <Route exact path="/profile" element={<Account />} />
          <Route exact path="shelters" element={<Shelters />} />
          <Route exact path="shelters/:id" element={<Shelter />} />
          <Route exact path="users" element={<Users />} />
          <Route exact path="users/:id" element={<User />} />
          <Route exact path="analytics" element={<Analytics />} />
        </Routes>
      </BrowserRouter>
    )
  );
};

export default Router;
