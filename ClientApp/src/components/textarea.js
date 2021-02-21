import React from "react";
import styled from "styled-components";

const Container = styled.div`
  position: relative;
  margin-left: ${(props) => props.center && "auto"};
  margin-right: ${(props) => props.center && "auto"};
  margin-bottom: 12px;
  padding: ${(props) => props.theme.padding.default};
`;

const TextAreaInput = styled.textarea`
  width: 100%;
  height: ${(props) => (props.height ? props.height : "auto")};
  background: ${(props) => props.theme.colors.dark};
  border: 2px dashed
    ${(props) =>
      props.valid ? props.theme.colors.white : props.theme.colors.danger};
  border-radius: ${(props) => props.theme.border.radius.button};
  color: ${(props) => props.theme.colors.white};
  resize: none;
  padding: ${(props) => props.theme.padding.medium};

  &:focus-visible {
    outline: none;
    box-shadow: none;
  }
`;

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

export function TextArea({
  value,
  onChange,
  validation,
  placeholder,
  center,
  height,
  type,
}) {
  const valid = validation.errors.length === 0;

  const errors = validation.errors.map((err) => {
    return <ValidationLabel key={err.id}>{err.message}</ValidationLabel>;
  });

  const valueChange = (e) => {
    onChange(e.target.value);
  };
  return (
    <Container center={center}>
      {!valid ? errors : null}
      <TextAreaInput
        height={height}
        placeholder={placeholder}
        value={value}
        valid={valid}
        type={type}
        onChange={valueChange}
      />
    </Container>
  );
}
