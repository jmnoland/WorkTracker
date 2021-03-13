import React from "react";
import styled from "styled-components";

const Container = styled.div`
  width: 30px;
`;

const Img = styled.img``;

export default function Icon({ onClick, src }) {
  return (
    <Container onClick={onClick}>
      <Img src={src} />
    </Container>
  );
}
