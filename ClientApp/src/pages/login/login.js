import React, { useState } from "react";
import { useHistory } from "react-router-dom";
import styled from "styled-components";
import { Input, Button } from "../../components";
import { UserLogin } from "../../services/auth";

const LoginContainer = styled.div`
  margin-left: 30%;
  margin-right: 30%;
  margin-top: 10%;
  box-shadow: ${(props) => props.theme.border.shadow};
  border-radius: ${(props) => props.theme.border.radius.default};
`;

const Title = styled.div`
  font-size: 50px;
  text-align: center;
`;
const TitleStart = styled.span`
  color: ${(props) => props.theme.colors.orange};
`;
const TitleEnd = styled.span`
  color: ${(props) => props.theme.colors.white};
`;

const Content = styled.div``;

export default function Login(props) {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);
  const [fieldValid, setFieldValid] = useState({ email: true, password: true });
  const history = useHistory();
  const { isLoggedIn, setLoggedIn } = props;

  const emailChange = (e) => {
    setEmail(e.target.value);
  };
  const passwordChange = (e) => {
    setPassword(e.target.value);
  };
  const handleLogin = async () => {
    const isValid = { ...fieldValid };
    // if (email === "" && !email) {
    //   setFieldValid({ ...fieldValid, email: false });
    //   isValid.email = false;
    // }
    // if (password === "" && !password) {
    //   setFieldValid({ ...fieldValid, password: false });
    //   isValid.password = false;
    // }
    if (isValid.email && isValid.password) {
      setLoading(true);
      try {
        await UserLogin(email, password);
        setLoggedIn(true);
        history.push("/");
      } catch (error) {
        if (error.response.status === 400) console.log("try again");
        setLoading(false);
      }
      setLoading(false);
    }
  };

  if (isLoggedIn) {
    return null;
  }

  return (
    <LoginContainer>
      <Title>
        <TitleStart>Work</TitleStart>
        <TitleEnd>Tracker</TitleEnd>
      </Title>
      <Content>
        <Input
          label="Email"
          position="above"
          center
          isValid={fieldValid.email}
          value={email}
          onChange={emailChange}
        />
        <Input
          label="Password"
          position="above"
          center
          isValid={fieldValid.password}
          type={"password"}
          value={password}
          onChange={passwordChange}
        />
        <Button primary loading={loading} onClick={handleLogin}>
          Login
        </Button>
      </Content>
    </LoginContainer>
  );
}
