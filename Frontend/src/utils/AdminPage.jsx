import { useAuth0 } from "@auth0/auth0-react";
import React, { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { authSettings } from "../configs/AuthSettings";

const AdminPage = ({ children }) => {
  const { user } = useAuth0();
  const navigate = useNavigate();

  /* inseamna ca are utilizator ca si rol, deci nu poate vedea partea de admin */
  useEffect(() => {
    if (user && user[authSettings.rolesKey].length === 0) {
      navigate("/");
    }
  }, [user, navigate]);

  return (
    <>{children}</>
  );
};

export default AdminPage;
