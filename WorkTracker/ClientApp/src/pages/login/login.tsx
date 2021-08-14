import React, { useState, useContext } from "react";
import styled from "styled-components";
import { useObject } from "../../helper";
import { LoginInput, Button, LoginTitle, InLineLink } from "../../components";
import { UserLogin, DemoLogin } from "../../services/auth";
import { UserDetailContext } from "../../context/userDetails";
import Register from "./register";

const LoginContainer = styled.div`
  width: 400px;
  margin-left: auto;
  margin-right: auto;
  margin-top: 10%;
  padding: ${(props) => props.theme.padding.large};
  box-shadow: ${(props) => props.theme.border.shadow};
  border-radius: ${(props) => props.theme.border.radius.default};
`;

const ButtonContainer = styled.div`
  margin-top: ${(props) => props.theme.margin.medium};
  margin-bottom: ${(props) => props.theme.margin.medium};
`;

const VersionNumber = styled.div`
  margin-left: auto;
  margin-right: auto;
  padding: ${(props) => props.theme.padding.medium};
  text-align: center;
  font-size: ${(props) => props.theme.font.size.small};
`;

const Content = styled.div``;

export default function Login(): JSX.Element {
  const [loading, setLoading] = useState(false);
  const [demoLoading, setDemoLoading] = useState(false);
  const [register, setRegister] = useState(false);
  const { isLoggedIn, setIsLoggedIn, setTokenDetails } = useContext(
    UserDetailContext
  );

  const initialValues = { email: "", password: "" };
  const fields = useObject(
    {
      email: {
        name: "email",
        value: "",
        validation: {
          rules: [
            {
              validate: (value: string) => {
                return value !== "" && value;
              },
              message: "Please enter an email",
            },
          ],
        },
      },
      password: {
        name: "password",
        value: "",
        validation: {
          rules: [
            {
              validate: (value: string) => {
                return value !== "" && value;
              },
              message: "Please enter a password",
            },
          ],
        },
      },
    },
    initialValues
  );

  const { email, password } = fields.data;

  const handleSubmit = async () => {
    const isValid = fields.validate();
    if (isValid) {
      setLoading(true);
      try {
        setTokenDetails(await UserLogin(email.value, password.value));
        fields.reset();
        setIsLoggedIn(true);
        setLoading(false);
      } catch {
        setLoading(false);
      }
    }
  };

  const handleDemoLogin = async () => {
    setDemoLoading(true);
    try {
      setTokenDetails(await DemoLogin());
      fields.reset();
      setIsLoggedIn(true);
      setDemoLoading(false);
    } catch {
      setDemoLoading(false);
    }
  };

  if (isLoggedIn) {
    return <></>;
  }

  if (register) {
    return <Register setRegister={setRegister} />;
  }

  return (
    <>
      <LoginContainer>
        <LoginTitle />
        <Content>
          <LoginInput label="Email" {...email} />
          <LoginInput label="Password" type={"password"} {...password} />
          <ButtonContainer>
            <Button
              center
              isLoginButton
              primary
              loading={loading}
              onClick={handleSubmit}
            >
              Login
            </Button>
            <Button
              center
              isLoginButton
              loading={demoLoading}
              onClick={handleDemoLogin}
            >
              Demo login
            </Button>
          </ButtonContainer>
          <div>
            Need an account?{" "}
            <InLineLink onClick={() => setRegister(true)}>
              Sign up here.{" "}
            </InLineLink>
          </div>
        </Content>
      </LoginContainer>
      <VersionNumber>Version 1.0.3</VersionNumber>
    </>
  );
}
