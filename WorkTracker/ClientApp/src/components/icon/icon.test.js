import React from "react";
import Icon from "./icon.tsx";
import { render, cleanup } from "@testing-library/react";

afterEach(cleanup);

describe("Icon component", () => {
  it("has src", () => {
    const { container } = render(<Icon id="test" src={"src"} />);
    const icon = container.querySelector('[id="test"]');
    expect(icon.firstChild.src).toBe("http://localhost/src");
  });
});
