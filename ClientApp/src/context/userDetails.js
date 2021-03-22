import React, { createContext, useState, useEffect } from "react";
import { decodeJwtToken } from "../helper";
import { GetDetails } from "../services/user";

export const UserDetailContext = createContext();

export const UserDetailProvider = ({ children }) => {
  const [userDetail, setUserDetail] = useState({});
  const [decodedToken, setDecodedToken] = useState(null);
  const [user, setUser] = useState(decodedToken ? decodedToken.nameid : null);
  const [permissions, setPermissions] = useState(
    decodedToken ? decodedToken.UserRole : []
  );
  const [isLoggedIn, setIsLoggedIn] = useState(
    decodedToken ? !!decodedToken : false
  );

  useEffect(() => {
    async function userDetailChange() {
      if (user) {
        setUserDetail(await GetDetails(user));
      }
    }
    userDetailChange();
  }, [user]);

  const logout = () => {
    setUser(null);
    setPermissions([]);
    setUserDetail({});
    setIsLoggedIn(false);
  };

  const setTokenDetails = (token) => {
    const decodedToken = decodeJwtToken(token);
    setDecodedToken(decodedToken);
    if (decodedToken) {
      setUser(decodedToken.nameid);
      setPermissions(decodedToken.UserRole);
    }
  };

  return (
    <UserDetailContext.Provider
      value={{
        user,
        userDetail,
        permissions,
        isLoggedIn,
        setUser,
        setIsLoggedIn,
        setTokenDetails,
        logout,
      }}
    >
      {children}
    </UserDetailContext.Provider>
  );
};
