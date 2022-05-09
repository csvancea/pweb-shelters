import React from "react";
import { Link, useLocation, useMatch, useResolvedPath } from "react-router-dom";
import logo from "../assets/logo.svg";
import { MdLeaderboard, MdLogout } from "react-icons/md";
import { GiBarracksTent } from "react-icons/gi"
import { HiUserCircle } from "react-icons/hi"
import Avatar from "react-avatar";
import { useAuth0 } from "@auth0/auth0-react";

export function CustomLink({ children, to, ...props }) {
  const resolved = useResolvedPath(to);
  const location = useLocation();
  const match =
    useMatch({ path: resolved.pathname, end: true }) ??
    location.pathname.includes(to);

  return (
    <div>
      <Link
        to={to}
        {...props}
        className={"nav-link" + (match ? " active" : "")}
      >
        {children}
      </Link>
    </div>
  );
}

const PageMenu = () => {
  const { logout, user } = useAuth0();

  return (
    <div className="admin-menu relative">
      <div className="logo-container">
        <img src={logo} alt="Weblib logo" />
      </div>
      <nav className="admin-nav">
        <div>
          <CustomLink to={"/shelters"}>
            <GiBarracksTent /> Shelters
          </CustomLink>
          <CustomLink to={"/users"}>
            <HiUserCircle /> Users
          </CustomLink>
          <CustomLink to={"/analytics"}>
            <MdLeaderboard /> Analytics
          </CustomLink>
        </div>
      </nav>
      <div className="admin-nav-bottom absolute bottom-10">
        <CustomLink to={"/profile"}>
          <Avatar name={user.nickname} round="24px" size="24px" />
          <p className="capitalize">{user.nickname}</p>
        </CustomLink>
        <Link className="nav-link active bg-black" to={"/"} onClick={() => {logout({ returnTo: window.location.origin }); }}>
          <MdLogout /> Sign out
        </Link>
      </div>
    </div>
  );
};

export default PageMenu;
