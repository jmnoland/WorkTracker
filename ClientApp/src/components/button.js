import React from "react";
import styled from "styled-components";
import Loading from "./loading";

const Container = styled.div`
  width: max-content;
  margin-left: ${(props) => props.center && "auto"};
  margin-right: ${(props) => props.center && "auto"};
  padding: ${(props) => (props.small ? "0px" : props.theme.padding.default)};
`;

const Button = styled.button`
  background: ${(props) =>
    props.primary ? props.theme.colors.orange : props.theme.colors.light};
  color: ${(props) => props.theme.colors.white};
  border: 5px solid
    ${(props) =>
      props.primary ? props.theme.colors.orange : props.theme.colors.light};
  border-radius: ${(props) => props.theme.border.radius.button};
  font-size: ${(props) => props.theme.font.size.default};
  font-weight: 600;
  width: 100px;

  &:hover {
    background: ${(props) =>
      props.primary
        ? props.theme.colors.orange_dark
        : props.theme.colors.white};
    border-color: ${(props) =>
      props.primary
        ? props.theme.colors.orange_dark
        : props.theme.colors.white};
    color: ${(props) => (props.primary ? "inherit" : props.theme.colors.dark)};
  }
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
    props.primary ? props.theme.colors.orange : props.theme.colors.light};
  color: ${(props) => (props.primary ? props.theme.colors.white : "inherit")};
  border: 5px solid
    ${(props) =>
      props.primary ? props.theme.colors.orange : props.theme.colors.light};
  border-radius: ${(props) => props.theme.border.radius.button};
  font-size: ${(props) => props.theme.font.size.default};
  font-weight: 600;
  width: 180px;

  &:hover {
    background: ${(props) =>
      props.primary
        ? props.theme.colors.orange_dark
        : props.theme.colors.white};
    border-color: ${(props) =>
      props.primary
        ? props.theme.colors.orange_dark
        : props.theme.colors.white};
    color: ${(props) => (props.primary ? "inherit" : props.theme.colors.dark)};
  }
  &:focus {
    outline: none;
    box-shadow: none;
  }
  &:focus-visible {
    outline: none;
    box-shadow: none;
  }
`;

const SmallButton = styled.button`
  background: ${(props) =>
    props.primary ? props.theme.colors.orange : props.theme.colors.light};
  color: ${(props) => props.theme.colors.white};
  border: 1px solid
    ${(props) =>
      props.primary ? props.theme.colors.orange : props.theme.colors.light};
  border-radius: ${(props) => props.theme.border.radius.button};
  font-size: ${(props) => props.theme.font.size.default};
  font-weight: 600;
  width: 100px;

  &:hover {
    background: ${(props) =>
      props.primary
        ? props.theme.colors.orange_dark
        : props.theme.colors.white};
    border-color: ${(props) =>
      props.primary
        ? props.theme.colors.orange_dark
        : props.theme.colors.white};
    color: ${(props) => (props.primary ? "inherit" : props.theme.colors.dark)};
  }
  &:focus {
    outline: none;
    box-shadow: none;
  }
  &:focus-visible {
    outline: none;
    box-shadow: none;
  }
`;

const InactivePrimary = styled.button`
  background: ${(props) => props.theme.colors.light};
  color: ${(props) => props.theme.colors.white};
  border: 1px solid ${(props) => props.theme.colors.light};
  border-radius: ${(props) => props.theme.border.radius.button};
  font-size: ${(props) => props.theme.font.size.default};
  font-weight: 600;
  width: 100px;

  &:hover {
    background: ${(props) => props.theme.colors.orange};
    border-color: ${(props) => props.theme.colors.orange};
  }
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
  isSmallButton,
  isInactivePrimary,
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
  if (isSmallButton) {
    return (
      <Container small center={center}>
        <SmallButton value={value} primary={primary} onClick={onClick}>
          {loading ? <Loading small primary={primary} /> : children}
        </SmallButton>
      </Container>
    );
  }
  if (isInactivePrimary) {
    return (
      <Container small center={center}>
        <InactivePrimary value={value} onClick={onClick}>
          {loading ? <Loading small primary={primary} /> : children}
        </InactivePrimary>
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
