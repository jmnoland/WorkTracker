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

const Message = styled.div`
  color: ${(props) => props.theme.colors.success};
`;

export default function Notification(): JSX.Element {
  const { content, visible } = useContext(NotificationContext);

  if (!visible) return <></>;
  return (
    <Container>
      <Message>{content}</Message>
    </Container>
  );
}
