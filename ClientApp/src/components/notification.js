import React, { useContext } from "react";
import styled from "styled-components";
import { NotificationContext } from "../context/notification";

const Container = styled.div`
  position: fixed;
  top: 72px;
  width: max-content;
  margin-left: auto;
  margin-right: auto;
`;

const Message = styled.div``;

export default function Notification() {
  const { content, visible } = useContext(NotificationContext);

  if (!visible) return null;
  return (
    <Container>
      <Message>{content}</Message>
    </Container>
  );
}
