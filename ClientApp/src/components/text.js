import React, { useState } from "react";
import styled from "styled-components";
import { theme } from "../constants/theme";
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

export function EditableText({ value, canEdit, height, onChange }) {
  const [canEdit, setCanEdit] = useState(false);
  const onClickHandler = (event) => {
    clearTimeout(timer);
    if (event.detail === 1) {
      timer = setTimeout(onClick, 200);
    } else if (event.detail === 2) {
      setCanEdit(true);
    }
  };
  return (
    <Container>
      {canEdit ? (
        <TextFieldInput height={height} onChange={onChange} value={value} />
      ) : (
        <Text value={value} onClick={onClickHandler}></Text>
      )}
    </Container>
  );
}
