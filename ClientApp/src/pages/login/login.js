import React, { useState } from "react";
import { useHistory } from "react-router-dom";
import styled from "styled-components";
import { useObject } from "../../helper";
import { Input, Button, Title, Link } from "../../components";
import { UserLogin } from "../../services/auth";
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

const Content = styled.div``;

export default function Login(props) {
  const [loading, setLoading] = useState(false);
  const [register, setRegister] = useState(false);

  const initialValues = { email: "", password: "" };
  const fields = useObject(
    {
      email: {
        name: "email",
        value: "",
        validation: {
          rules: [
            {
              validate: (value) => {
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
              validate: (value) => {
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
  const { isLoggedIn, setLoggedIn } = props;

  const handleSubmit = async () => {
    const isValid = fields.validate();
    if (isValid) {
      setLoading(true);
      await UserLogin(email.value, password.value);
      setLoggedIn(true);
      setLoading(false);
    }
  };

  if (isLoggedIn) {
    return null;
  }

  if (register) {
    return <Register setRegister={setRegister} />;
  }

  return (
    <LoginContainer>
      <Title />
      <Content>
        <Input label="Email" position="above" center {...email} />
        <Input
          label="Password"
          position="above"
          center
          type={"password"}
          {...password}
        />
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
        </ButtonContainer>
        <div>
          Need an account?{" "}
          <Link onClick={() => setRegister(true)}>Sign up here.</Link>
          Forgot your <Link>password? </Link>
        </div>
      </Content>
    </LoginContainer>
  );
}
