import React, { useState } from "react";
import styled from "styled-components";
import { Input, Button, LoginTitle, InLineLink } from "../../components";
import { useObject } from "../../helper";
import { UserRegister } from "../../services/auth";

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

export default function Register({ setRegister }) {
  const [loading, setLoading] = useState(false);
  const initialValues = {
    email: "",
    name: "",
    password: "",
    confirmPassword: "",
  };
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
      name: {
        name: "name",
        value: "",
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
      confirmPassword: {
        name: "confirmPassword",
        value: "",
        validation: {
          rules: [
            {
              validate: (value) => {
                return value !== "" && value;
              },
              message: "Please retype your password",
            },
            {
              validate: (value, data) => {
                return value === data.password.value;
              },
              message: "Passwords must match",
            },
          ],
        },
      },
    },
    initialValues
  );

  const { email, name, password, confirmPassword } = fields.data;

  const handleRegister = async () => {
    if (fields.validate()) {
      setLoading(true);
      await UserRegister(email.value, name.value, password.value);
      setLoading(false);
      setRegister(false);
    }
  };

  return (
    <LoginContainer>
      <LoginTitle />
      <Content>
        <Input label="Email" position="above" center {...email} />
        <Input label="Name" position="above" center {...name} />
        <Input
          label="Password"
          position="above"
          center
          type={"password"}
          {...password}
        />
        <Input
          label="Confirm password"
          position="above"
          center
          type={"password"}
          {...confirmPassword}
        />
        <ButtonContainer>
          <Button
            center
            isLoginButton
            primary
            loading={loading}
            onClick={handleRegister}
          >
            Sign up
          </Button>
        </ButtonContainer>
        <div>
          <InLineLink onClick={() => setRegister(false)}>
            Back to login?{" "}
          </InLineLink>
        </div>
      </Content>
    </LoginContainer>
  );
}
