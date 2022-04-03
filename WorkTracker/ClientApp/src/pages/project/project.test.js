import { Provider } from "react-redux";
import store from "../../redux/store";
import Project from "./project";
import renderer from "react-test-renderer";
import React from "react";

describe("Project page", () => {
  it('renders correctly', () => {
    const element = (
      <Provider store={store}>
        <Project />
      </Provider>
    );
    const tree = renderer
      .create(element)
      .toJSON();
    expect(tree).toMatchSnapshot();
  });
});
