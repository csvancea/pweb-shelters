import React from "react";
import { Link, useLocation, useMatch, useResolvedPath } from "react-router-dom";
import logo from "../assets/logo.png";
import { MdLeaderboard } from "react-icons/md";
import { GiBarracksTent } from "react-icons/gi"
import { HiUserCircle } from "react-icons/hi"
import { FaHouseUser } from "react-icons/fa"
import AdminOnly from "./AdminOnly";
import PageMenuFooter from "./PageMenuFooter";

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
  return (
    <div className="admin-menu relative">
      <div className="logo-container">
        <img src={logo} alt="logo" style={{"margin-top": "75px", "max-width": "30%"}} />
      </div>
      <nav className="admin-nav">
        <div>
          <CustomLink to={"/profile"}>
            <FaHouseUser /> Profile
          </CustomLink>
          <CustomLink to={"/shelters"}>
            <GiBarracksTent /> Shelters
          </CustomLink>
          <AdminOnly>
            <CustomLink to={"/users"}>
              <HiUserCircle /> Users
            </CustomLink>
            <CustomLink to={"/analytics"}>
              <MdLeaderboard /> Analytics
            </CustomLink>
          </AdminOnly>
        </div>
      </nav>
      <div className="admin-nav-bottom absolute bottom-10">
        <PageMenuFooter />
      </div>
    </div>
  );
};

export default PageMenu;
