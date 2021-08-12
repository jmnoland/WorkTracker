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
  min-height: 600px;
  height: fit-content;
  background: ${(props) => props.theme.colors.background};
  padding: ${(props) => props.theme.padding.large};
  box-shadow: ${(props) => props.theme.border.shadow};
  border-radius: ${(props) => props.theme.border.radius.default};

  @media (max-width: 600px) {
    width: 100%;
  }
`;

const ModalHeading = styled.div`
  padding-bottom: ${(props) => props.theme.padding.large};
  font-size: ${(props) => props.theme.font.size.large};
  display: flex;
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

const ModalFooterLeft = styled.div`
  position: absolute;
  bottom: 0;
  left: 0;
  padding-left: ${(props) => props.theme.padding.large};
  padding-top: ${(props) => props.theme.padding.large};
  padding-bottom: ${(props) => props.theme.padding.large};
`;

const Title = styled.div`
  flex: 1;
`;

const CloseButton = styled.div`
  cursor: pointer;
  float: right;
`;

const SVG = styled.svg`
  width: 40px;
  fill: ${(props) => props.theme.colors.white};

  &:hover {
    fill: ${(props) => props.theme.colors.orange};
  }
`;

interface ModalProps {
    title: React.ReactNode;
    visible: boolean;
    footer: React.ReactNode;
    footerLeft: React.ReactNode;
    onClose: () => void;
    children: React.ReactNode;
}

export function Modal({
  title,
  visible,
  footer,
  footerLeft,
  onClose,
  children,
}: ModalProps) {
  if (!visible) {
    return null;
  }

  return (
    <ModalContainer>
      <ModalWindow>
        <ModalHeading>
          <Title>{title}</Title>
          <CloseButton onClick={onClose}>
            <SVG xmlns="http://www.w3.org/2000/svg" viewBox="0 0 32 32">
              <path d="M 5 5 L 5 27 L 27 27 L 27 5 Z M 7 7 L 25 7 L 25 25 L 7 25 Z M 11.6875 10.3125 L 10.28125 11.71875 L 14.5625 16 L 10.21875 20.34375 L 11.625 21.75 L 15.96875 17.40625 L 20.28125 21.71875 L 21.6875 20.3125 L 17.375 16 L 21.625 11.75 L 20.21875 10.34375 L 15.96875 14.59375 Z" />
            </SVG>
          </CloseButton>
        </ModalHeading>
        <ModalContent>{children}</ModalContent>
        <ModalFooterLeft>{footerLeft}</ModalFooterLeft>
        <ModalFooter>{footer}</ModalFooter>
      </ModalWindow>
    </ModalContainer>
  );
}
