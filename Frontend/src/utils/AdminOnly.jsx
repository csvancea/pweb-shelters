import React from 'react';
import { useAuth0 } from '@auth0/auth0-react';
import { authSettings } from "../AuthSettings";

function AdminOnly({ children }) {
  const { user } = useAuth0();
  const isAdmin = user && (user[authSettings.rolesKey].length !== 0);
  
  if (!isAdmin) {
    return <></>;
  }
  return <>{children}</>;
}

export default AdminOnly;
