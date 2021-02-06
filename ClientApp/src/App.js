import React from "react";
import { Route } from "react-router";
import styled, { ThemeProvider, createGlobalStyle } from "styled-components";
import { theme } from "./constants/theme";
import Login from "./pages/login/login";
import Dashboard from "./pages/dashboard/dashboard";
import NavMenu from "./components/layout/navMenu";
import { decodeJwtToken, getToken } from "./helper";

const GlobalStyle = createGlobalStyle`
    body
    {
        background: ${(props) => props.theme.colors.background};
        min-height:100%;
        min-width:100%;
        overflow: hidden;
    }
    html
    {
        height:100%;
    }
`;

const AppContainer = styled.div`
  background: ${(props) => props.theme.colors.background};
  color: ${(props) => props.theme.colors.white};
`;

export default function App() {
  const token = getToken();
  const isLoggedIn = token ? decodeJwtToken(token) : false;

  return (
    <ThemeProvider theme={theme}>
      <GlobalStyle />
      <AppContainer>
        {isLoggedIn ? (
          <NavMenu>
            <Route exact path="/" component={Dashboard} />
            <Route exact path="/login" component={Login} />
          </NavMenu>
        ) : (
          <Login />
        )}
      </AppContainer>
    </ThemeProvider>
  );
}
