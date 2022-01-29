import React from "react";
import "./subtext.scss";

export default function BaseSubText({ children }: { children: React.ReactNode }): JSX.Element {
  return <div className="subtext">{children}</div>;
}
