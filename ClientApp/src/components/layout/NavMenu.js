import React from "react";
import styled from "styled-components";
import { MainTitle, NavLink } from "../";

const Navbar = styled.div`
  box-shadow: ${(props) => props.theme.border.shadow};
  border-radius: ${(props) => props.theme.border.radius.default};
`;

const Container = styled.div`
  display: flex;
  justify-content: space-between;
`;

const NavbarBrand = styled.div``;

const LinkContainer = styled.div`
  display: flex;
`;

const NavItem = styled.div`
  margin-top: auto;
  margin-bottom: auto;
`;

const NavLinkContainer = styled.div`
  cursor: pointer;
  color: white;
  font-size: 20px;
  margin-right: 20px;
`;

export default function NavMenu(props) {
  const { isLoggedIn } = props;

  if (!isLoggedIn) {
    return null;
  }

  return (
    <header>
      <Navbar>
        <Container>
          <NavbarBrand>
            <MainTitle />
          </NavbarBrand>
          <LinkContainer>
            <NavItem>
              <NavLinkContainer>
                <NavLink to="/">Board</NavLink>
              </NavLinkContainer>
            </NavItem>
            <NavItem>
              <NavLinkContainer>
                <NavLink to="/report">Reports</NavLink>
              </NavLinkContainer>
            </NavItem>
          </LinkContainer>
          <div>Profile</div>
        </Container>
      </Navbar>
    </header>
  );
}
