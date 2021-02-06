import React from "react";
import styled from "styled-components";

const Container = styled.div`
  width: max-content;
  margin-left: ${(props) => props.center && "auto"};
  margin-right: ${(props) => props.center && "auto"};
  padding: ${(props) => props.theme.padding.default};
`;

const Input = styled.input`
  background: ${(props) => props.theme.colors.white};
  border: 5px solid ${(props) => props.theme.colors.white};
  border-radius: ${(props) => props.theme.border.radius.button};

  &:focus-visible {
    outline: none;
    box-shadow: none;
  }
`;

const Label = styled.span``;

const LabelAbove = styled.div``;

export default function BaseInput({
  value,
  onChange,
  label,
  position,
  center,
  type,
}) {
  const inputLabel =
    position === "above" ? (
      <LabelAbove>{label}</LabelAbove>
    ) : (
      <Label>{label}</Label>
    );

  return (
    <Container center={center}>
      {inputLabel}
      <Input value={value} type={type} onChange={onChange} />
    </Container>
  );
}
