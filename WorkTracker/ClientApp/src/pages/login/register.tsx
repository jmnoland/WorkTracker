import React, { useState } from "react";
import { Input, Button, LoginTitle, InLineLink, GenericContainer } from "../../components";
import { useForm } from "../../helper";
import { UserRegister } from "../../services/auth";
import { registerFields } from "./fields";
import "./login.scss";

const LoginContainer = GenericContainer("login-container");
const ButtonContainer = GenericContainer("button-container");
const Content = GenericContainer();

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
