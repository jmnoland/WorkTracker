import React, { useState, useContext } from "react";
import { useForm } from "../../helper";
import { LoginInput, Button, LoginTitle, InLineLink, GenericContainer } from "../../components";
import { UserLogin, DemoLogin } from "../../services/auth";
import { UserDetailContext } from "../../context/userDetails";
import Register from "./register";
import { loginFields } from './fields';
import "./login.scss";

const LoginContainer = GenericContainer("login-container");

const ButtonContainer = GenericContainer("button-container");

const VersionNumber = GenericContainer("version-number");

const Content = GenericContainer();

export default function Login(): JSX.Element {
  const [loading, setLoading] = useState(false);
  const [demoLoading, setDemoLoading] = useState(false);
  const [register, setRegister] = useState(false);
  const { isLoggedIn, setIsLoggedIn, setTokenDetails } = useContext(
    UserDetailContext
  );

  const initialValues = { email: "", password: "" };
  const obj = useForm(
    loginFields,
    initialValues
  );

  const { email, password } = obj.form;

  const handleSubmit = async () => {
    const isValid = obj.validate();
    if (isValid) {
      setLoading(true);
      try {
        setTokenDetails(await UserLogin(email.value, password.value));
        obj.reset();
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
      obj.reset();
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
      <VersionNumber>Version 1.0.4</VersionNumber>
    </>
  );
}
