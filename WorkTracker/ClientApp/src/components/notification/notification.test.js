import React, { useContext } from "react";
import {
  NotificationContext,
  NotificationProvider,
} from "../../context/notification";
import Notification from "./notification.tsx";
import { render, cleanup, fireEvent } from "@testing-library/react";

afterEach(cleanup);

function WrappedComponent() {
  const contextValue = useContext(NotificationContext);

  const onClick = () => {
    contextValue.setContent("test message");
  };
  return (
    <div>
      <Notification />
      <button id="test" onClick={onClick}>
        Test
      </button>
    </div>
  );
}

describe("Notification component", () => {
  it("hidden when content empty", () => {
    const component = (
      <NotificationProvider>
        <WrappedComponent />
      </NotificationProvider>
    );
    const { container, rerender } = render(component);
    rerender(component);
    expect(container.firstChild.children.length === 1).toBe(true);
  });
  it("visible when content set", () => {
    const component = (
      <NotificationProvider>
        <WrappedComponent />
      </NotificationProvider>
    );
    const { container, rerender } = render(component);
    const btn = container.querySelector('[id="test"]');
    fireEvent.click(btn);
    rerender(component);
    expect(container.firstChild.children.length > 1).toBe(true);
  });
});
