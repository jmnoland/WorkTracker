import React from "react";
import styled from "styled-components";
import { Error } from "../../types";
import "./input.scss";

const Container = styled.div<{ center?: boolean, isLogin?: boolean }>`
  margin-left: ${(props) => props.center && "auto"};
  margin-right: ${(props) => props.center && "auto"};
`;

const TextAreaInput = styled.input<{ height?: string }>`
  height: ${(props) => (props.height ? props.height : "auto")};
`;

const Label = styled.span``;

const LabelAbove = styled.div``;

interface BaseInputProps {
  id?: string,
  value: string | number;
  onChange: (val: unknown) => void;
  label?: string;
  position?: string;
  errors?: Error[];
  isLogin?: boolean;
  center?: boolean;
  type?: string;
}

export default function BaseInput({
  id,
  value,
  onChange,
  errors,
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

  const valid = errors?.length === 0;

  const errorText = errors?.map((err) => {
    return <span className="label-validation" key={err.id}>{err.message}</span>;
  });

  const valueChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    onChange(e.target.value);
  };
  return (
    <Container id={id} className="input-container" center={center}>
      {inputLabel}
      {!valid ? errorText : null}
      <input className={valid ? "input-base valid-border" : "input-base invalid-border"} value={value} type={type} onChange={valueChange} />
    </Container>
  );
}

export function LoginInput({
  id,
  value,
  onChange,
  errors,
  label,
  type
}: BaseInputProps): JSX.Element {
  const valid = errors?.length === 0;

  const errorText = errors?.map((err) => {
    return <span className="label-validation" key={err.id}>{err.message}</span>;
  });

  const valueChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    onChange(e.target.value);
  };
  return (
    <Container id={id} className="input-container" center isLogin>
      {<LabelAbove>{label}</LabelAbove>}
      {!valid ? errorText : null}
      <input className={valid ? "input-base valid-border" : "input-base invalid-border"} value={value} type={type} onChange={valueChange} />
    </Container>
  );
}

interface TextFieldInputProps {
  id?: string,
  value: string;
  height?: string;
  useRef?: React.RefObject<HTMLInputElement> | null | undefined;
  errors?: Error[];
  onBlur?: () => void;
  onChange: (val: string) => void;
  placeholder?: string;
  center?: boolean;
}

export function TextFieldInput({
  id,
  value,
  height,
  useRef,
  onBlur,
  onChange,
  errors,
  placeholder,
  center,
}: TextFieldInputProps): JSX.Element {
  const valid = errors !== undefined ? errors.length === 0 : true;

  const errorText = errors?.map((err) => {
    return <span className="label-validation" key={err.id}>{err.message}</span>;
  });
  
  const valueChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    onChange(e.target.value);
  };
  const classes = `input-text-area ${valid ? "valid-border" : "invalid-border"}`
  return (
    <Container id={id} className="input-container" center={center}>
      {!valid ? errorText : null}
      <TextAreaInput
        ref={useRef}
        className={classes}
        height={height}
        placeholder={placeholder}
        value={value}
        onBlur={onBlur}
        onChange={valueChange}
      />
    </Container>
  );
}
