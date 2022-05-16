import React, { useState, useEffect, useCallback } from "react";
import Avatar from "react-avatar";
import { MdLogout } from "react-icons/md";
import { Link } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";
import { routes } from "../configs/Api";
import axiosInstance from "../configs/Axios";

const PageMenuFooter = () => {
  const { logout, getAccessTokenSilently } = useAuth0();
  const [userData, setUserData] = useState({});

  const getUser = useCallback(async () => {
    const accessToken = await getAccessTokenSilently();
    axiosInstance
      .get(routes.profiles.getProfile(0), {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      })
      .then(({ data }) => setUserData(data));
  }, [getAccessTokenSilently]);

  useEffect(() => {
    getUser();
  }, [getUser]);

  return (
    <div>
      <div className="nav-link">
        <Avatar name={userData.profile?.name} round="24px" size="24px" />
        <p className="capitalize">{userData.profile?.name}</p>
      </div>
      <Link className="nav-link active bg-black" to={"#"} onClick={() => {logout({ returnTo: window.location.origin }); }}>
        <MdLogout /> Sign out
      </Link>
    </div>
  );
};

export default PageMenuFooter;
