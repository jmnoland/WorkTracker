import React, { useState, useRef, useEffect } from "react";
import styled from "styled-components";
import { TextFieldInput, TextArea } from "../index";

const Container = styled.div``;

const TextContainer = styled.div<{ height?: string, margin?: string, useBackground?: boolean }>`
  height: ${(props) => (props.height ? props.height : "auto")};
  margin: ${(props) => (props.margin ? props.margin : null)};
`;

export function Text({
    value,
    height,
    margin,
    useBackground,
    onClick
}: {
    value: string,
    height?: string,
    margin?: string,
    useBackground?: boolean,
    onClick?: () => void,
}): JSX.Element {
  return (
    <Container>
      <TextContainer
        className={`text-container ${useBackground ? "text-background" : ""}`}
        height={height}
        margin={margin}
        useBackground={useBackground}
        onClick={onClick}
      >
        {value}
      </TextContainer>
    </Container>
  );
}

export function EditableText({
  value,
  type,
  height,
  margin,
  useBackground,
  onChange,
  edit,
}: {
  value: string,
  type?: string,
  height?: string,
  margin?: string,
  useBackground?: boolean,
  onChange: (val: string) => void,
  edit?: boolean,
}): JSX.Element {
  const [canEdit, setCanEdit] = useState(!!edit);
  const inputRef = type !== "area" ? useRef<HTMLInputElement | null>(null): undefined;
  const areaRef = type === "area" ? useRef<HTMLTextAreaElement | null>(null): undefined;

  useEffect(() => {
    if (canEdit) {
      if (type === "area") areaRef?.current?.focus();
      else inputRef?.current?.focus();
    }
  }, [canEdit]);

  const onClickHandler = () => {
    setCanEdit(true);
  };

  const editableElement =
    type === "area" ? (
      <TextArea
        useRef={areaRef}
        height={height}
        onChange={onChange}
        value={value}
        onBlur={() => setCanEdit(false)}
      />
    ) : (
      <TextFieldInput
        useRef={inputRef}
        height={height}
        onChange={onChange}
        value={value}
        onBlur={() => setCanEdit(false)}
      />
    );

  return (
    <Container>
      {canEdit ? (
        editableElement
      ) : (
        <Text
          height={height}
          margin={margin}
          value={value}
          useBackground={useBackground}
          onClick={onClickHandler}
        ></Text>
      )}
    </Container>
  );
}
