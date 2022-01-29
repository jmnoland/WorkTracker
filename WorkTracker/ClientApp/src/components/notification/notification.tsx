import React, { useContext } from "react";
import { NotificationContext } from "../../context/notification";
import "./notification.scss";

export default function Notification(): JSX.Element {
  const { content, visible } = useContext(NotificationContext);

  if (!visible) return <></>;
  return (
    <div className="notification-container">
      <div className="notification-message">{content}</div>
    </div>
  );
}
