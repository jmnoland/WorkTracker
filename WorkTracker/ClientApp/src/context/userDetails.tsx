import React, { createContext, useState, useEffect } from "react";
import { UserDetail } from '../types';
import { decodeJwtToken } from "../helper";
import { GetDetails } from "../services/user";

interface UserDetailContext {
    userDetail: UserDetail | null;
    user: string | null;
    permissions: string[];
    isLoggedIn: boolean;
    setUser: (user: string | null) => void;
    setIsLoggedIn: (isLoggedIn: boolean) => void;
    setTokenDetails: (token: string) => void;
    logout: () => void;
};

export const UserDetailContext = createContext<UserDetailContext>({
    userDetail: null,
    user: null,
    permissions: [],
    isLoggedIn: false,
    setUser: () => {},
    setIsLoggedIn: () => {},
    setTokenDetails: () => {},
    logout: () => {},
});

export const UserDetailProvider = ({ children }: { children: React.ReactNode }) => {
  const initialUserDetail: UserDetail = {
    organisation: null,
    states: [],
    teams: [],
    users: [],
  };
  const token = localStorage.getItem("X-User-Token");
  const [userDetail, setUserDetail] = useState(initialUserDetail);
  const [decodedToken, setDecodedToken] = useState(
    token ? decodeJwtToken(token) : null
  );
  const [user, setUser] = useState(decodedToken ? decodedToken.nameid : null);
  const [permissions, setPermissions] = useState(
    decodedToken ? decodedToken.role : []
  );
  const [isLoggedIn, setIsLoggedIn] = useState(
    decodedToken ? !!decodedToken : false
  );

  useEffect(() => {
    async function userDetailChange() {
      if (user) {
        setUserDetail(await GetDetails());
      }
    }
    userDetailChange();
  }, [user]);

  const logout = () => {
    setUser(null);
    setPermissions([]);
    setUserDetail(initialUserDetail);
    setIsLoggedIn(false);
    localStorage.removeItem("X-User-Token");
  };

  const setTokenDetails = (token: string) => {
    const decodedToken = decodeJwtToken(token);
    setDecodedToken(decodedToken);
    if (decodedToken) {
      setUser(decodedToken.nameid);
      setPermissions(decodedToken.role);
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
