import React, { useEffect } from "react";
import { Route, Routes, Navigate } from "react-router-dom";
import Analytics from "../pages/Analytics";
import Shelter from "../pages/Shelter";
import Shelters from "../pages/Shelters";
import Users from "../pages/Users";
import User from "../pages/User";
import AdminPage from "./AdminPage";
import { useAuth0 } from "@auth0/auth0-react";
import { routes } from "../configs/Api";
import axiosInstance from "../configs/Axios";
import { useNavigate } from "react-router-dom";

const Router = () => {
  const { user, isAuthenticated, loginWithRedirect, getAccessTokenSilently } = useAuth0();
  const navigate = useNavigate();

  useEffect(() => {
    if (!isAuthenticated) {
      loginWithRedirect();
    } else {
      (async () => {
        const accessToken = await getAccessTokenSilently();
        axiosInstance
          .post(routes.profiles.setupProfile, {Email: user.email, Name: user.name}, {
            headers: {
              Authorization: `Bearer ${accessToken}`,
            },
          })
          .then(() => navigate("/profile/#setup"))
          .catch((e) => {
            /* 409: conflict = user already setup! */
            if (e.response?.status !== 409) {
              throw e;
            }
          })
      })();
    }
  }, [user, isAuthenticated, loginWithRedirect, getAccessTokenSilently, navigate]);

  return (
    isAuthenticated && (
      <Routes>
        <Route exact path="/" element={<Navigate to="/shelters" />} />
        <Route exact path="profile" element={<User self/>} />
        <Route exact path="shelters" element={<Shelters />} />
        <Route exact path="shelters/:id" element={<AdminPage><Shelter /></AdminPage>} />
        <Route exact path="users" element={<AdminPage><Users /></AdminPage>} />
        <Route exact path="users/:id" element={<User />} />
        <Route exact path="analytics" element={<AdminPage><Analytics /></AdminPage>} />
      </Routes>
    )
  );
};

export default Router;
