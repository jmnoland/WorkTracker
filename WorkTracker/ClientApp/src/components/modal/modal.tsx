import React from "react";
import "./modal.scss";

interface ModalProps {
    title: React.ReactNode;
    visible: boolean;
    footer: React.ReactNode;
    footerLeft?: React.ReactNode;
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
}: ModalProps): JSX.Element {
  if (!visible) {
    return <></>;
  }

  return (
    <div className="custom-modal-container">
      <div className="custom-modal-window">
        <div className="custom-modal-heading">
          <div className="custom-modal-title">{title}</div>
          <div className="custom-modal-close-button" onClick={onClose}>
            <svg className="custom-modal-svg" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 32 32">
              <path d="M 5 5 L 5 27 L 27 27 L 27 5 Z M 7 7 L 25 7 L 25 25 L 7 25 Z M 11.6875 10.3125 L 10.28125 11.71875 L 14.5625 16 L 10.21875 20.34375 L 11.625 21.75 L 15.96875 17.40625 L 20.28125 21.71875 L 21.6875 20.3125 L 17.375 16 L 21.625 11.75 L 20.21875 10.34375 L 15.96875 14.59375 Z" />
            </svg>
          </div>
        </div>
        <div className="custom-modal-content">{children}</div>
        <div className="custom-modal-footer-left">{footerLeft}</div>
        <div className="custom-modal-footer">{footer}</div>
      </div>
    </div>
  );
}
