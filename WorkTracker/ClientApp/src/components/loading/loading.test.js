import { getSize } from "./loading.tsx";
import { cleanup } from "@testing-library/react";

afterEach(cleanup);

describe("Loading component", () => {
  it("get size small", () => {
    const dimensions = getSize(true);
    expect(dimensions.width === "20px" && dimensions.height === "20px").toBe(
      true
    );
  });
  it("get size large", () => {
    const dimensions = getSize(false);
    expect(dimensions.width === "64px" && dimensions.height === "64px").toBe(
      true
    );
  });
});
