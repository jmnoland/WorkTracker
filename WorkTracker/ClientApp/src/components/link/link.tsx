import React from "react";
import { Link } from "react-router-dom";
import './link.scss';

interface NavLinkProps {
    to: string;
    children: React.ReactNode;
    onClick?: () => void;
}

interface LinkProps {
    children: React.ReactNode;
    onClick: () => void;
}

export function NavLink({ to, children }: NavLinkProps): JSX.Element {
  return (
    <div className="link-container">
      <Link to={to}>{children}</Link>
    </div>
  );
}

export function InLineLink({ onClick, children }: LinkProps): JSX.Element {
  return (
    <div className="inline-link-container">
      <span onClick={onClick}>{children}</span>
    </div>
  );
}

export function BaseLink({ onClick, children }: LinkProps): JSX.Element {
  return (
    <div className="link-container">
      <span onClick={onClick}>{children}</span>
    </div>
  );
}
