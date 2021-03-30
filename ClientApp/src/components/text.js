import React, { useState, useRef, useEffect } from "react";
import styled from "styled-components";
import { TextFieldInput } from "./index";

const Container = styled.div``;

const TextContainer = styled.div``;

export function Text({ value, onClick }) {
  return (
    <Container>
      <TextContainer onClick={onClick}>{value}</TextContainer>
    </Container>
  );
}

export function EditableText({ value, height, onChange }) {
  const [canEdit, setCanEdit] = useState(false);
  const inputRef = useRef();

  useEffect(() => {
    if (canEdit) inputRef.current.focus();
  }, [canEdit]);

  const onClickHandler = () => {
    setCanEdit(true);
  };

  return (
    <Container>
      {canEdit ? (
        <TextFieldInput
          useRef={inputRef}
          height={height}
          onChange={onChange}
          value={value}
          onBlur={() => setCanEdit(false)}
        />
      ) : (
        <Text value={value} onClick={onClickHandler}></Text>
      )}
    </Container>
  );
}
