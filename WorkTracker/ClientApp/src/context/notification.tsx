import React, { createContext, useState, useEffect } from "react";

interface NotificationContext {
    content: string | null;
    visible: boolean;
    setTime: (time: number) => void;
    setContent: (content: string | null) => void;
};

export const NotificationContext = createContext<NotificationContext>({
    content: null,
    visible: false,
    setTime: () => null,
    setContent: () => null,
});

export const NotificationProvider = ({ children }: { children: React.ReactNode }): JSX.Element => {
  const [content, setContent] = useState<string | null>(null);
  const [time, setTime] = useState<number | null>(null);
  const [visible, setVisible] = useState<boolean>(false);

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
