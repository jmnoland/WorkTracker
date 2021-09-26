import React from "react";
import Loading from "../loading/loading";
import "./button.scss";

interface BaseButtonProps {
    onClick: () => void;
    center?: boolean;
    loading?: boolean;
    primary?: boolean;
    children: React.ReactNode;
    isLoginButton?: boolean;
    isSmallButton?: boolean;
    isDeleteButton?: boolean;
    isInactivePrimary?: boolean;
}

export default function BaseButton({
  onClick,
  center,
  loading,
  primary,
  children,
  isLoginButton,
  isSmallButton,
  isDeleteButton,
  isInactivePrimary,
}: BaseButtonProps): JSX.Element {
  if (isLoginButton) {
    return (
      <div className={center ? "center" : ""}>
        <button className={primary ? "button-login-primary" : "button-login"} onClick={onClick}>
          {loading ? <Loading small primary={primary} /> : children}
        </button>
      </div>
    );
  }
  if (isDeleteButton) {
    return (
      <div className={center ? "center" : ""}>
        <button className="button-delete" onClick={onClick}>
          {loading ? <Loading small primary={primary} /> : children}
        </button>
      </div>
    );
  }
  if (isSmallButton) {
    return (
      <div className={`${center ? "center" : ""} small`}>
        <button className={primary ? "button-small-primary" : "button-small"} onClick={onClick}>
          {loading ? <Loading small primary={primary} /> : children}
        </button>
      </div>
    );
  }
  if (isInactivePrimary) {
    return (
      <div className={`${center ? "center" : ""} small`}>
        <button className="button-inactive-primary" onClick={onClick}>
          {loading ? <Loading small primary={primary} /> : children}
        </button>
      </div>
    );
  }
  return (
    <div className={center ? "center" : ""}>
      <button className={primary ? "button-primary" : "button"} onClick={onClick}>
        {loading ? <Loading small primary={primary} /> : children}
      </button>
    </div>
  );
}
