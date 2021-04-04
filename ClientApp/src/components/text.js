import React, { useState, useRef, useEffect } from "react";
import styled from "styled-components";
import { TextFieldInput, TextArea } from "./index";

const Container = styled.div``;

const TextContainer = styled.div`
  word-break: break-word;
  overflow-y: auto;
  text-overflow: ellipsis;
  height: ${(props) => (props.height ? props.height : "auto")};
  padding-right: 7px;
  margin-bottom: 10px;

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

export function Text({ value, height, onClick }) {
  return (
    <Container>
      <TextContainer height={height} onClick={onClick}>
        {value}
      </TextContainer>
    </Container>
  );
}

export function EditableText({ value, type, height, onChange }) {
  const [canEdit, setCanEdit] = useState(false);
  const inputRef = useRef();

  useEffect(() => {
    console.log(inputRef);
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
        <Text height={height} value={value} onClick={onClickHandler}></Text>
      )}
    </Container>
  );
}
