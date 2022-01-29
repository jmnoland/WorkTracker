import React from "react";
import { Route, Switch } from "react-router";
import { ThemeProvider, createGlobalStyle } from "styled-components";
import Login from "./pages/login/login";
import Report from "./pages/report/report";
import Board from "./pages/board/board";
import { Theme, theme } from "./constants/theme";
import NavMenu from "./components/layout/NavMenu";
import { GenericContainer } from "./components";
import { NotificationProvider } from "./context/notification";
import { Provider } from 'react-redux'
import store from "./redux/store";
import './App.scss';
import { useAppSelector } from "./redux/hooks";

const GlobalStyle = createGlobalStyle<{theme: Theme}>`
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

const AppContainer = GenericContainer("app-container");
const ContentContainer = GenericContainer("app-content-container");

function Content({ children } : { children: React.ReactNode }) {
  const isLoggedIn = useAppSelector((state) => state.user.isLoggedIn);
  if (isLoggedIn) {
    return <ContentContainer>{children}</ContentContainer>;
  }
  return null;
}

export default function App(): JSX.Element {
  return (
    <Provider store={store}>
      <ThemeProvider theme={theme}>
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
      </ThemeProvider>
    </Provider>
  );
}
