import React from "react";
import styled from "styled-components";
import Loading from "./loading";

const Container = styled.div`
  width: max-content;
  margin-left: ${(props) => props.center && "auto"};
  margin-right: ${(props) => props.center && "auto"};
  padding: ${(props) => props.theme.padding.default};
`;

const Button = styled.button`
  background: ${(props) =>
    props.primary ? props.theme.colors.orange : props.theme.colors.white};
  color: ${(props) => (props.primary ? props.theme.colors.white : "inherit")};
  border: 5px solid
    ${(props) =>
      props.primary ? props.theme.colors.orange : props.theme.colors.white};
  border-radius: ${(props) => props.theme.border.radius.button};
  font-size: ${(props) => props.theme.font.size.default}
  font-weight: 600;
  width: 100px;

  &:focus {
    outline: none;
    box-shadow: none;
  }
  &:focus-visible {
    outline: none;
    box-shadow: none;
  }
`;

const LoginButton = styled.button`
  background: ${(props) =>
    props.primary ? props.theme.colors.orange : props.theme.colors.white};
  color: ${(props) => (props.primary ? props.theme.colors.white : "inherit")};
  border: 5px solid
    ${(props) =>
      props.primary ? props.theme.colors.orange : props.theme.colors.white};
  border-radius: ${(props) => props.theme.border.radius.button};
  font-size: ${(props) => props.theme.font.size.default}
  font-weight: 600;
  width: 180px;

  &:focus {
    outline: none;
    box-shadow: none;
  }
  &:focus-visible {
    outline: none;
    box-shadow: none;
  }
`;

export default function BaseButton({
  value,
  onClick,
  center,
  loading,
  primary,
  children,
  isLoginButton,
}) {
  if (isLoginButton) {
    return (
      <Container center={center}>
        <LoginButton value={value} primary={primary} onClick={onClick}>
          {loading ? <Loading small primary={primary} /> : children}
        </LoginButton>
      </Container>
    );
  }
  return (
    <Container center={center}>
      <Button value={value} primary={primary} onClick={onClick}>
        {loading ? <Loading small primary={primary} /> : children}
      </Button>
    </Container>
  );
}
