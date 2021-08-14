import React, { useContext } from "react";
import styled from "styled-components";
import { UserDetailContext } from "../../context/userDetails";
import { MainTitle } from "..";

const Navbar = styled.div`
  box-shadow: ${(props) => props.theme.border.shadow};
  border-radius: ${(props) => props.theme.border.radius.default};
`;

const Container = styled.div`
  display: flex;
  justify-content: space-between;
`;

const NavbarBrand = styled.div``;

// const LinkContainer = styled.div`
//   display: flex;
// `;

// const NavItem = styled.div`
//   margin-top: auto;
//   margin-bottom: auto;
// `;

// const NavLinkContainer = styled.div`
//   cursor: pointer;
//   color: white;
//   font-size: 20px;
//   margin-right: 20px;
// `;

const ProfileContainer = styled.div`
  margin-top: auto;
  margin-bottom: auto;
  margin-right: 20px;
  cursor: pointer;
  font-weight: 500;

  &:hover {
    color: ${(props) => props.theme.colors.orange};
  }
`;

export default function NavMenu(): JSX.Element {
  const { isLoggedIn, logout }: { isLoggedIn: boolean, logout: () => void } = useContext(UserDetailContext);

  if (!isLoggedIn) {
    return <></>;
  }

  return (
    <header>
      <Navbar>
        <Container>
          <NavbarBrand>
            <MainTitle />
          </NavbarBrand>
          {/* <LinkContainer>
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
          </LinkContainer> */}
          <ProfileContainer onClick={logout}>Logout</ProfileContainer>
        </Container>
      </Navbar>
    </header>
  );
}
