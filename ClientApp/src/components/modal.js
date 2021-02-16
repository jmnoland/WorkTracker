import React from "react";
import styled from "styled-components";

const ModalContainer = styled.div`
  position: fixed;
  width: 100%;
  height: 100%;
  top: 0;
  background-color: rgba(0, 0, 0, 0.4);
`;

const ModalWindow = styled.div`
  position: relative;
  margin-left: auto;
  margin-right: auto;
  margin-top: 10%;
  width: 600px;
  height: 600px;
  background: ${(props) => props.theme.colors.background};
  padding: ${(props) => props.theme.padding.large};
  box-shadow: ${(props) => props.theme.border.shadow};
  border-radius: ${(props) => props.theme.border.radius.default};
`;

const ModalHeading = styled.div`
  padding-bottom: ${(props) => props.theme.padding.large};
  font-size: ${(props) => props.theme.font.size.large};
`;

const ModalContent = styled.div`
  padding-top: ${(props) => props.theme.padding.large};
  font-size: ${(props) => props.theme.font.size.default};
`;

const ModalFooter = styled.div`
  position: absolute;
  bottom: 0;
  right: 0;
  padding-right: ${(props) => props.theme.padding.large};
  padding-top: ${(props) => props.theme.padding.large};
  padding-bottom: ${(props) => props.theme.padding.large};
`;

const CloseButton = styled.div`
  cursor: pointer;
  float: right;
`;

export function Modal({ title, visible, footer, onClose, children }) {
  if (!visible) {
    return null;
  }

  return (
    <ModalContainer>
      <ModalWindow>
        <ModalHeading>
          <span>{title}</span>
          <CloseButton onClick={onClose}>Close</CloseButton>
        </ModalHeading>
        <ModalContent>{children}</ModalContent>
        <ModalFooter>{footer}</ModalFooter>
      </ModalWindow>
    </ModalContainer>
  );
}
