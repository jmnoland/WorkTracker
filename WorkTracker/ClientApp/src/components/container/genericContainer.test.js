import React from "react";
import CreateGenericContainer from "./genericContainer.tsx";
import { render, cleanup } from "@testing-library/react";

afterEach(cleanup);

describe("Container component", () => {
  it("has display name", () => {
    const comp = CreateGenericContainer("classNameTest");
    expect(comp.displayName).toBe("ContainerclassNameTest");
  });
  it("children visible", () => {
    const Comp = CreateGenericContainer("classNameTest");
    const { container } = render(<Comp id="test">Test</Comp>);
    const comp = container.querySelector('[id="test"]');
    expect(comp.innerHTML === "Test").toBe(false);
  });
  it("has classes", () => {
    const Comp = CreateGenericContainer("classNameTest");
    const { container } = render(<Comp id="test">Test</Comp>);
    const comp = container.querySelector('[id="test"]');
    let containsClass = false;
    comp.classList.forEach((val) => {
      if (val === "classNameTest") containsClass = true;
    });
    expect(containsClass).toBe(true);
  });
});
