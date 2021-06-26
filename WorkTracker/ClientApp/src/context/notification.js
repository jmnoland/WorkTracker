import React, { createContext, useState, useEffect } from "react";

export const NotificationContext = createContext();

export const NotificationProvider = ({ children }) => {
  const [content, setContent] = useState(null);
  const [time, setTime] = useState(null);
  const [visible, setVisible] = useState(false);

  useEffect(() => {
    if (content) {
      setVisible(true);
      setTimeout(() => {
        setVisible(false);
        setContent(null);
      }, time ?? 3000);
    }
  }, [content]);

  return (
    <NotificationContext.Provider
      value={{
        content,
        visible,
        setTime,
        setContent,
      }}
    >
      {children}
    </NotificationContext.Provider>
  );
};
