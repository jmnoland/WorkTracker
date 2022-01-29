import React from "react";
import { TextArea } from "./textarea";
import { screen, render, cleanup, fireEvent } from "@testing-library/react";

afterEach(cleanup);

describe("TextArea Input component", () => {
  it("value can change", () => {
    let newValue = "";
    render(
      <TextArea
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
    render(<TextArea id="test" errors={[]} value={"defaultVal"} />);
    const input = screen.getByRole("textbox");
    expect(input.className.includes("valid")).toBe(true);
  });
  it("is invalid with errors", () => {
    render(
      <TextArea
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
      <TextArea
        id="test"
        label={"label"}
        errors={errors}
        value={"defaultVal"}
      />
    );
    let containsErrors = false;
    const div = container.firstChild;
    if (
      div.children[0].innerHTML === "err1" &&
      div.children[1].innerHTML === "err2" &&
      div.children[2].innerHTML === "err3"
    )
      containsErrors = true;
    expect(containsErrors).toBe(true);
  });
});
