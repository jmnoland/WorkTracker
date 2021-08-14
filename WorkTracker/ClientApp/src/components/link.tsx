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

interface NavLinkProps {
    to: string;
    children: React.ReactNode;
    onClick: () => void;
}

interface LinkProps {
    children: React.ReactNode;
    onClick: () => void;
}

export function NavLink({ to, children }: NavLinkProps): JSX.Element {
  return (
    <LinkContainer>
      <Link to={to}>{children}</Link>
    </LinkContainer>
  );
}

export function InLineLink({ onClick, children }: LinkProps): JSX.Element {
  return (
    <InLineLinkContainer>
      <span onClick={onClick}>{children}</span>
    </InLineLinkContainer>
  );
}

export function BaseLink({ onClick, children }: LinkProps): JSX.Element {
  return (
    <LinkContainer>
      <span onClick={onClick}>{children}</span>
    </LinkContainer>
  );
}
