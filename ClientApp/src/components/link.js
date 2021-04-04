import React from "react";
import styled from "styled-components";
import { Link } from "react-router-dom";

const LinkContainer = styled.div`
  > * {
    color: white;
    text-decoration: none;
  }
`;

const InLineLinkContainer = styled.span`
  cursor: pointer;
  > * {
    color: white;
    text-decoration: none;
  }
  &:hover {
    text-decoration: underline;
  }
`;

export function NavLink({ to, children }) {
  return (
    <LinkContainer>
      <Link to={to}>{children}</Link>
    </LinkContainer>
  );
}

export function InLineLink({ onClick, children }) {
  return (
    <InLineLinkContainer>
      <span onClick={onClick}>{children}</span>
    </InLineLinkContainer>
  );
}

export function BaseLink({ onClick, children }) {
  return (
    <LinkContainer>
      <span onClick={onClick}>{children}</span>
    </LinkContainer>
  );
}
