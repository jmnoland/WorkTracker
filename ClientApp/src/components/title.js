import React from "react";
import styled from "styled-components";

const Title = styled.div`
  font-size: 50px;
  text-align: center;
  margin-bottom: ${(props) => props.theme.margin.large};
`;
const TitleStart = styled.span`
  color: ${(props) => props.theme.colors.orange};
`;
const TitleEnd = styled.span`
  color: ${(props) => props.theme.colors.white};
`;

export default function BaseTitle() {
  return (
    <Title>
      <TitleStart>Work</TitleStart>
      <TitleEnd>Tracker</TitleEnd>
    </Title>
  );
}
