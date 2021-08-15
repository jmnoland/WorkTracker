import React from "react";
import styled from "styled-components";

const LoginTitleContainer = styled.div`
  font-size: 50px;
  text-align: center;
  margin-bottom: ${(props) => props.theme.margin.large};
`;

const MainTitleContainer = styled.div`
  font-size: 30px;
  padding: 10px 10px 10px 30px;
`;

const TitleStart = styled.span`
  color: ${(props) => props.theme.colors.orange};
`;
const TitleEnd = styled.span`
  color: ${(props) => props.theme.colors.white};
`;

export function LoginTitle(): JSX.Element {
  return (
    <LoginTitleContainer>
      <TitleStart>Work</TitleStart>
      <TitleEnd>Tracker</TitleEnd>
    </LoginTitleContainer>
  );
}

export function MainTitle(): JSX.Element {
  return (
    <MainTitleContainer>
      <TitleStart>Work</TitleStart>
      <TitleEnd>Tracker</TitleEnd>
    </MainTitleContainer>
  );
}
