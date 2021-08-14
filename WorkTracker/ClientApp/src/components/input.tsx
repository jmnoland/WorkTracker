import React from "react";
import styled from "styled-components";
import { Error } from '../types';

const Container = styled.div<{ center?: boolean, isLogin?: boolean }>`
  position: relative;
  margin-left: ${(props) => props.center && "auto"};
  margin-right: ${(props) => props.center && "auto"};
  margin-bottom: 12px;
  padding: ${(props) => props.theme.padding.default};
`;

const Input = styled.input<{ isLogin?: boolean, valid?: boolean }>`
  width: ${(props) => (props.isLogin ? "190px" : "100%")};
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

const TextAreaInput = styled.input<{ valid?: boolean, height?: string }>`
  width: 100%;
  height: ${(props) => (props.height ? props.height : "auto")};
  text-overflow: ellipsis;
  background: ${(props) => props.theme.colors.dark};
  border: 1px solid
    ${(props) =>
      props.valid ? props.theme.colors.light : props.theme.colors.danger};
  border-radius: ${(props) => props.theme.border.radius.button};
  color: ${(props) => props.theme.colors.white};
  padding: ${(props) => props.theme.padding.medium};

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

interface BaseInputProps {
    value: string | number;
    onChange: (val: string | number) => void;
    validation: { errors: Error[] };
    label?: string;
    position?: string;
    isLogin?: boolean;
    center?: boolean;
    type?: string;
}

export default function BaseInput({
  value,
  onChange,
  validation,
  label,
  position,
  center,
  type,
}: BaseInputProps): JSX.Element {
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

  const valueChange = (e: React.ChangeEvent<HTMLInputElement>) => {
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

export function LoginInput({
    value,
    onChange,
    validation,
    label,
    type
}: BaseInputProps): JSX.Element {
  const valid = validation.errors.length === 0;

  const errors = validation.errors.map((err) => {
    return <ValidationLabel key={err.id}>{err.message}</ValidationLabel>;
  });

  const valueChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    onChange(e.target.value);
  };
  return (
    <Container center isLogin>
      {<LabelAbove>{label}</LabelAbove>}
      {!valid ? errors : null}
      <Input value={value} valid={valid} type={type} onChange={valueChange} />
    </Container>
  );
}

interface TextFieldInputProps {
  value: string;
  height?: string;
  useRef?: React.RefObject<HTMLInputElement> | null | undefined;
  onBlur?: () => void;
  onChange: (val: string) => void;
  validation?: { errors: Error[] };
  placeholder?: string;
  center?: boolean;
}

export function TextFieldInput({
  value,
  height,
  useRef,
  onBlur,
  onChange,
  validation,
  placeholder,
  center,
}: TextFieldInputProps): JSX.Element {
  const valid = validation ? validation.errors.length === 0 : true;

  const errors =
    validation &&
    validation.errors.map((err) => {
      return <ValidationLabel key={err.id}>{err.message}</ValidationLabel>;
    });

  const valueChange = (e: React.ChangeEvent<HTMLInputElement>) => {
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
        onBlur={onBlur}
        onChange={valueChange}
      />
    </Container>
  );
}
