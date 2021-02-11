import React from "react";
import styled from "styled-components";

const Container = styled.div`
  width: max-content;
  position: relative;
  margin-left: ${(props) => props.center && "auto"};
  margin-right: ${(props) => props.center && "auto"};
  margin-bottom: 12px;
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
  font-size: 11px;
  margin-top: 7px;
  margin-left: 2px;
  color: ${(props) => props.theme.colors.danger};
  display: flex;
  justify-content: flex-end;
  position: absolute;
  bottom: -12px;
`;

export default function BaseInput({
  value,
  onChange,
  validation,
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

  const valid = validation.errors.length === 0;

  const errors = validation.errors.map((err) => {
    return <ValidationLabel key={err.id}>{err.message}</ValidationLabel>;
  });

  const valueChange = (e) => {
    onChange(e.target.value);
  };
  return (
    <Container center={center}>
      {inputLabel}
      {!valid ? errors : null}
      <Input value={value} valid={valid} type={type} onChange={valueChange} />
    </Container>
  );
}