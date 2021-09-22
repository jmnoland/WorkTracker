import React, { useState } from "react";
import styled from "styled-components";
import { Input, Button, LoginTitle, InLineLink } from "../../components";
import { useForm } from "../../helper";
import { UserRegister } from "../../services/auth";
import { registerFields } from "./fields";

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

export default function Register({ setRegister }
  : { setRegister: (val: boolean) => void }
): JSX.Element {
  const [loading, setLoading] = useState(false);
  const initialValues = {
    email: "",
    name: "",
    password: "",
    confirmPassword: "",
  };
  const obj = useForm(
    registerFields,
    initialValues
  );

  const { email, name, password, confirmPassword } = obj.form;

  const handleRegister = async () => {
    if (obj.validate()) {
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
