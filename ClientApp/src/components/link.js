import React from "react";
import styled, { css } from "styled-components";
import { Link } from "react-router-dom";

const LinkContainer = styled.div`
  > * {
    color: white;
    text-decoration: none;
  }
`;

export function NavLink({ to, children }) {
  return (
    <LinkContainer>
      <Link to={to}>{children}</Link>
    </LinkContainer>
  );
}

export function BaseLink({ onClick, children }) {
  return (
    <LinkContainer>
      <span onClick={onClick}>{children}</span>
    </LinkContainer>
  );
}
