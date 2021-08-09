import React, { useState, useRef, useEffect } from "react";
import styled from "styled-components";
import { TextFieldInput, TextArea } from "./index";

const Container = styled.div``;

const TextContainer = styled.div`
  word-break: break-word;
  text-overflow: ellipsis;
  height: ${(props) => (props.height ? props.height : "auto")};
  margin-bottom: 10px;
  margin: ${(props) => (props.margin ? props.margin : null)};

  padding: ${(props) =>
    props.useBackground ? props.theme.padding.medium : "7px"};
  color: ${(props) =>
    props.useBackground ? props.theme.colors.white : "inherit"};
  border: 1px solid
    ${(props) =>
      props.useBackground
        ? props.theme.colors.light
        : props.theme.colors.background};
  background: ${(props) =>
    props.useBackground ? props.theme.colors.dark : "inherit"};
  border-radius: ${(props) =>
    props.useBackground ? props.theme.border.radius.button : "0px"};

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

export function Text({ value, height, margin, useBackground, onClick }) {
  return (
    <Container>
      <TextContainer
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
}) {
  const [canEdit, setCanEdit] = useState(!!edit);
  const inputRef = useRef();

  useEffect(() => {
    if (canEdit) inputRef.current.focus();
  }, [canEdit]);

  const onClickHandler = () => {
    setCanEdit(true);
  };

  const editableElement =
    type === "area" ? (
      <TextArea
        useRef={inputRef}
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
