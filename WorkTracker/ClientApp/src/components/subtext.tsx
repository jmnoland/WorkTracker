import React from "react";
import styled from "styled-components";

const SubText = styled.div`
  font-size: 12px;
`;

export default function BaseSubText({ children }: { children: React.ReactNode }): JSX.Element {
  return <SubText>{children}</SubText>;
}
