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
  border: 1px solid
    ${(props) =>
      props.valid ? props.theme.colors.light : props.theme.colors.danger};
  border-radius: ${(props) => props.theme.border.radius.button};
  color: ${(props) => props.theme.colors.white};
  resize: none;
  padding: ${(props) => props.theme.padding.medium};

  &:focus-visible {
    outline: none;
    box-shadow: none;
  }
  ::-webkit-scrollbar {
    width: 10px;
  }
  ::-webkit-scrollbar-track {
    background: ${(props) => props.theme.colors.lighter};
  }
  ::-webkit-scrollbar-thumb {
    background: ${(props) => props.theme.colors.light};
  }
  ::-webkit-scrollbar-thumb:hover {
    background: #4c4c4c;
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
  onBlur,
  validation,
  placeholder,
  center,
  height,
  useRef,
  type,
}) {
  const valid = validation ? validation.errors.length === 0 : true;

  const errors =
    validation &&
    validation.errors.map((err) => {
      return <ValidationLabel key={err.id}>{err.message}</ValidationLabel>;
    });

  const valueChange = (e) => {
    onChange(e.target.value);
  };
  return (
    <Container center={center}>
      {!valid ? errors : null}
      <TextAreaInput
        ref={useRef}
        height={height}
        placeholder={placeholder}
        value={value}
        valid={valid}
        type={type}
        onChange={valueChange}
        onBlur={onBlur}
      />
    </Container>
  );
}
