import React, { useContext } from "react";
import { Route, Switch } from "react-router";
import styled, { ThemeProvider, createGlobalStyle } from "styled-components";
import Login from "./pages/login/login";
import Report from "./pages/report/report";
import Board from "./pages/board/board";
import { theme } from "./constants/theme";
import NavMenu from "./components/layout/navMenu";
import { UserDetailProvider, UserDetailContext } from "./context/userDetails";
import { NotificationProvider } from "./context/notification";

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

const ContentContainer = styled.div`
  position: absolute;
  width: 100%;
`;

function Content({ children }) {
  const { isLoggedIn } = useContext(UserDetailContext);
  if (isLoggedIn) {
    return <ContentContainer>{children}</ContentContainer>;
  }
  return null;
}

export default function App() {
  return (
    <ThemeProvider theme={theme}>
      <UserDetailProvider>
        <NotificationProvider>
          <GlobalStyle />
          <AppContainer>
            <NavMenu />
            <Content>
              <Switch>
                <Route exact path="/" component={Board} />
                <Route exact path="/report" component={Report} />
              </Switch>
            </Content>
            <Login />
          </AppContainer>
        </NotificationProvider>
      </UserDetailProvider>
    </ThemeProvider>
  );
}
