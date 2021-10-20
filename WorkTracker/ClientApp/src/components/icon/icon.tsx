import React from "react";
import styled from "styled-components";

const Container = styled.div<{ width?: string }>`
  width: ${(props) => props.width ?? "30"}px;
`;

const Img = styled.img``;

export default function Icon({ onClick, width, src }: {
    onClick: () => void,
    width?: string,
    src: string,
}): JSX.Element {
  return (
    <Container width={width} onClick={onClick}>
      <Img src={src} />
    </Container>
  );
}
