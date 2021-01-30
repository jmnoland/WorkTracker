import React, { Component } from "react";
import { Route } from "react-router";
import Login from "./pages/login/login";
import NavMenu from "./components/layout/NavMenu";

import "./custom.css";

export default class App extends Component {
  render() {
    return (
      <NavMenu>
        <Route exact path="/" component={Login} />
      </NavMenu>
    );
  }
}
