import React from "react";
import PageHeader from "./PageHeader";
import PageMenu from "./PageMenu";

const PageLayout = ({ children }) => {
  return (
    <div className="layout">
      <PageMenu />
      <div className="content">
        <PageHeader />
        {children}
      </div>
    </div>
  );
};

export default PageLayout;
