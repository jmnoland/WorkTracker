import React from "react";
import styled from "styled-components";
import { Error } from '../../types';
import "./textarea.scss";

const TextAreaInput = styled.textarea<{ height?: string }>`
  height: ${(props) => (props.height ? props.height : "auto")};
`;

interface TextAreaProps {
    value: string | number;
    onChange: (val: string) => void;
    onBlur?: () => void;
    errors?: Error[];
    placeholder?: string;
    center?: boolean;
    height?: string;
    useRef?: React.RefObject<HTMLTextAreaElement> | null;
    type?: string;
}

export function TextArea({
  value,
  onChange,
  onBlur,
  errors,
  placeholder,
  center,
  height,
  useRef,
}: TextAreaProps): JSX.Element {
  const valid = errors?.length === 0;

  const errorText = errors?.map((err) => {
    return <span className="textarea-validation-label" key={err.id}>{err.message}</span>;
  });

  const valueChange = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
    onChange(e.target.value);
  };
  return (
    <div className={`textarea-container ${center ? "textarea-center" : ""}`}>
      {!valid ? errorText : null}
      <TextAreaInput
        className={`textarea-input ${valid ? "valid-border": "invalid-border"}`}
        ref={useRef}
        height={height}
        placeholder={placeholder}
        value={value}
        onChange={valueChange}
        onBlur={onBlur}
      />
    </div>
  );
}
