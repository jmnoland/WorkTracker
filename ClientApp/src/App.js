import React, { useState } from "react";
import { Route, Switch } from "react-router";
import styled, { ThemeProvider, createGlobalStyle } from "styled-components";
import { theme } from "./constants/theme";
import Login from "./pages/login/login";
import Report from "./pages/report/report";
import Board from "./pages/board/board";
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

function Content({ isLoggedIn, children }) {
  if (isLoggedIn) {
    return <div>{children}</div>;
  }
  return null;
}

export default function App() {
  const token = getToken();
  const [isLoggedIn, setIsLoggedIn] = useState(
    token ? decodeJwtToken(token) : false
  );

  return (
    <ThemeProvider theme={theme}>
      <GlobalStyle />
      <AppContainer>
        <NavMenu isLoggedIn={isLoggedIn} />
        <Content isLoggedIn={isLoggedIn}>
          <Switch>
            <Route exact path="/" component={Board} />
            <Route exact path="/report" component={Report} />
          </Switch>
        </Content>
        <Login isLoggedIn={isLoggedIn} setLoggedIn={setIsLoggedIn} />
      </AppContainer>
    </ThemeProvider>
  );
}
