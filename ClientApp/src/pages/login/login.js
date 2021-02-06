import React, { useState } from "react";
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

export default function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);

  const emailChange = (e) => {
    setEmail(e.target.value);
  };
  const passwordChange = (e) => {
    setPassword(e.target.value);
  };
  const handleLogin = async () => {
    setLoading(true);
    await UserLogin(email, password);
    setLoading(false);
  };

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
          value={email}
          onChange={emailChange}
        />
        <Input
          label="Password"
          position="above"
          center
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
