import React from "react";
import Button from "./button.tsx";
import { render, cleanup } from "@testing-library/react";

afterEach(cleanup);

describe("Button component", () => {
  it("when not loading children are visible", () => {
    const { container } = render(<Button id="test">Save</Button>);
    const btn = container.querySelector('[id="test"]');
    expect(btn.innerHTML === "Save").toBe(true);
  });
  it("when loading children are not visible", () => {
    const { container } = render(
      <Button id="test" loading>
        Save
      </Button>
    );
    const btn = container.querySelector('[id="test"]');
    expect(btn.innerHTML === "Save").toBe(false);
  });
  it("when loading is disabled", () => {
    const { container } = render(
      <Button id="test" loading>
        Save
      </Button>
    );
    const btn = container.querySelector('[id="test"]');
    expect(btn.disabled).toBe(true);
  });
  it("is not disabled", () => {
    const { container } = render(<Button id="test">Save</Button>);
    const btn = container.querySelector('[id="test"]');
    expect(btn.disabled).toBe(false);
  });
  it("is centered", () => {
    const { container } = render(<Button center>Save</Button>);
    let isCentered = false;
    container.firstChild.classList.forEach((val) => {
      if (val.includes("center")) isCentered = true;
    });
    expect(isCentered).toBe(true);
  });
  it("is primary", () => {
    const { container } = render(
      <Button id="test" primary>
        Save
      </Button>
    );
    const btn = container.querySelector('[id="test"]');
    let isPrimary = false;
    btn.classList.forEach((val) => {
      if (val.includes("primary")) isPrimary = true;
    });
    expect(isPrimary).toBe(true);
  });
  it("is login", () => {
    const { container } = render(
      <Button id="test" isLoginButton>
        Save
      </Button>
    );
    const btn = container.querySelector('[id="test"]');
    let isLogin = false;
    btn.classList.forEach((val) => {
      if (val.includes("login")) isLogin = true;
    });
    expect(isLogin).toBe(true);
  });
  it("is small", () => {
    const { container } = render(
      <Button id="test" isSmallButton>
        Save
      </Button>
    );
    const btn = container.querySelector('[id="test"]');
    let isSmall = false;
    btn.classList.forEach((val) => {
      if (val.includes("small")) isSmall = true;
    });
    expect(isSmall).toBe(true);
  });
  it("is inactive", () => {
    const { container } = render(
      <Button id="test" isInactivePrimary>
        Save
      </Button>
    );
    const btn = container.querySelector('[id="test"]');
    let isInactive = false;
    btn.classList.forEach((val) => {
      if (val.includes("inactive")) isInactive = true;
    });
    expect(isInactive).toBe(true);
  });
  it("is delete", () => {
    const { container } = render(
      <Button id="test" isDeleteButton>
        Save
      </Button>
    );
    const btn = container.querySelector('[id="test"]');
    let isDelete = false;
    btn.classList.forEach((val) => {
      if (val.includes("delete")) isDelete = true;
    });
    expect(isDelete).toBe(true);
  });
});
