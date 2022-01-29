import React from "react";
import Loading from "../loading/loading";
import "./button.scss";

interface BaseButtonProps {
    id?: string;
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
  id,
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
        <button id={id} className={primary ? "button-login-primary" : "button-login"} disabled={loading} onClick={onClick}>
          {loading ? <Loading small primary={primary} /> : children}
        </button>
      </div>
    );
  }
  if (isDeleteButton) {
    return (
      <div className={center ? "center" : ""}>
        <button id={id} className="button-delete" disabled={loading} onClick={onClick}>
          {loading ? <Loading small primary={primary} /> : children}
        </button>
      </div>
    );
  }
  if (isSmallButton) {
    return (
      <div className={`${center ? "center" : ""} small`}>
        <button id={id} className={primary ? "button-small-primary" : "button-small"} disabled={loading} onClick={onClick}>
          {loading ? <Loading small primary={primary} /> : children}
        </button>
      </div>
    );
  }
  if (isInactivePrimary) {
    return (
      <div className={`${center ? "center" : ""} small`}>
        <button id={id} className="button-inactive-primary" disabled={loading} onClick={onClick}>
          {loading ? <Loading small primary={primary} /> : children}
        </button>
      </div>
    );
  }
  return (
    <div className={center ? "center" : ""}>
      <button id={id} className={primary ? "button-primary" : "button"} disabled={loading} onClick={onClick}>
        {loading ? <Loading small primary={primary} /> : children}
      </button>
    </div>
  );
}
