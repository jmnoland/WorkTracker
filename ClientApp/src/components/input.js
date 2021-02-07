import React from "react";
import styled from "styled-components";

const Container = styled.div`
  width: max-content;
  position: relative;
  margin-left: ${(props) => props.center && "auto"};
  margin-right: ${(props) => props.center && "auto"};
  padding: ${(props) => props.theme.padding.default};
`;

const Input = styled.input`
  background: ${(props) => props.theme.colors.white};
  border: 2px solid
    ${(props) =>
      props.valid ? props.theme.colors.white : props.theme.colors.danger};
  border-radius: ${(props) => props.theme.border.radius.button};

  &:focus-visible {
    outline: none;
    box-shadow: none;
  }
`;

const Label = styled.span``;

const LabelAbove = styled.div``;

const ValidationLabel = styled.span`
  display: flex;
  justify-content: flex-end;
  position: absolute;
  right: 10px;
  top: 5px;
`;

export default function BaseInput({
  value,
  onChange,
  isValid,
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
  const valid = typeof isValid === "boolean" ? isValid : isValid(value);

  return (
    <Container center={center}>
      {inputLabel}
      {!valid ? <ValidationLabel>{valid}</ValidationLabel> : null}
      <Input value={value} valid={valid} type={type} onChange={onChange} />
    </Container>
  );
}
