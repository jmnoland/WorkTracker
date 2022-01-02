import React from "react";
import BaseInput, { LoginInput, TextFieldInput } from "./input.tsx";
import { screen, render, cleanup, fireEvent } from "@testing-library/react";

afterEach(cleanup);

describe("Base Input component", () => {
  it("value can change", () => {
    let newValue = "";
    render(
      <BaseInput
        id="test"
        onChange={(e) => (newValue = e)}
        value={"defaultVal"}
      />
    );
    const input = screen.getByRole("textbox");
    fireEvent.change(input, {
      target: { value: "new value" },
    });
    expect(newValue).toBe("new value");
  });
  it("is valid without errors", () => {
    render(<BaseInput id="test" errors={[]} value={"defaultVal"} />);
    const input = screen.getByRole("textbox");
    expect(input.className.includes("valid")).toBe(true);
  });
  it("is invalid with errors", () => {
    render(
      <BaseInput
        id="test"
        errors={[{ id: "err1", message: "err" }]}
        value={"defaultVal"}
      />
    );
    const input = screen.getByRole("textbox");
    expect(input.className.includes("invalid")).toBe(true);
  });
  it("contains label", () => {
    const { container } = render(
      <BaseInput id="test" label={"label"} value={"defaultVal"} />
    );
    const div = container.querySelector('[id="test"]');
    expect(div.firstChild.innerHTML).toBe("label");
  });
  it("contains errors", () => {
    const errors = [
      { id: "err1", message: "err1" },
      { id: "err2", message: "err2" },
      { id: "err3", message: "err3" },
    ];
    const { container } = render(
      <BaseInput
        id="test"
        label={"label"}
        errors={errors}
        value={"defaultVal"}
      />
    );
    const div = container.querySelector('[id="test"]');
    let containsErrors = false;
    if (
      div.children[1].innerHTML === "err1" &&
      div.children[2].innerHTML === "err2" &&
      div.children[3].innerHTML === "err3"
    )
      containsErrors = true;
    expect(containsErrors).toBe(true);
  });
});

describe("Login Input component", () => {
  it("value can change", () => {
    let newValue = "";
    render(
      <LoginInput
        id="test"
        onChange={(e) => (newValue = e)}
        value={"defaultVal"}
      />
    );
    const input = screen.getByRole("textbox");
    fireEvent.change(input, {
      target: { value: "new value" },
    });
    expect(newValue).toBe("new value");
  });
  it("is valid without errors", () => {
    render(<LoginInput id="test" errors={[]} value={"defaultVal"} />);
    const input = screen.getByRole("textbox");
    expect(input.className.includes("valid")).toBe(true);
  });
  it("is invalid with errors", () => {
    render(
      <LoginInput
        id="test"
        errors={[{ id: "err1", message: "err" }]}
        value={"defaultVal"}
      />
    );
    const input = screen.getByRole("textbox");
    expect(input.className.includes("invalid")).toBe(true);
  });
  it("contains label", () => {
    const { container } = render(
      <LoginInput id="test" label={"label"} value={"defaultVal"} />
    );
    const div = container.querySelector('[id="test"]');
    expect(div.firstChild.innerHTML).toBe("label");
  });
  it("contains errors", () => {
    const errors = [
      { id: "err1", message: "err1" },
      { id: "err2", message: "err2" },
      { id: "err3", message: "err3" },
    ];
    const { container } = render(
      <LoginInput
        id="test"
        label={"label"}
        errors={errors}
        value={"defaultVal"}
      />
    );
    const div = container.querySelector('[id="test"]');
    let containsErrors = false;
    if (
      div.children[1].innerHTML === "err1" &&
      div.children[2].innerHTML === "err2" &&
      div.children[3].innerHTML === "err3"
    )
      containsErrors = true;
    expect(containsErrors).toBe(true);
  });
});

describe("Text Input component", () => {
  it("value can change", () => {
    let newValue = "";
    render(
      <TextFieldInput
        id="test"
        onChange={(e) => (newValue = e)}
        value={"defaultVal"}
      />
    );
    const input = screen.getByRole("textbox");
    fireEvent.change(input, {
      target: { value: "new value" },
    });
    expect(newValue).toBe("new value");
  });
  it("is valid without errors", () => {
    render(<TextFieldInput id="test" errors={[]} value={"defaultVal"} />);
    const input = screen.getByRole("textbox");
    expect(input.className.includes("valid")).toBe(true);
  });
  it("is invalid with errors", () => {
    render(
      <TextFieldInput
        id="test"
        errors={[{ id: "err1", message: "err" }]}
        value={"defaultVal"}
      />
    );
    const input = screen.getByRole("textbox");
    expect(input.className.includes("invalid")).toBe(true);
  });
  it("contains errors", () => {
    const errors = [
      { id: "err1", message: "err1" },
      { id: "err2", message: "err2" },
      { id: "err3", message: "err3" },
    ];
    const { container } = render(
      <TextFieldInput
        id="test"
        label={"label"}
        errors={errors}
        value={"defaultVal"}
      />
    );
    const div = container.querySelector('[id="test"]');
    let containsErrors = false;
    if (
      div.children[0].innerHTML === "err1" &&
      div.children[1].innerHTML === "err2" &&
      div.children[2].innerHTML === "err3"
    )
      containsErrors = true;
    expect(containsErrors).toBe(true);
  });
});
