import React, { useRef, useEffect } from "react";
import styled from "styled-components";

const Container = styled.div<{ height: number }>`
  min-height: ${(props) => props.height}px;
  width: 100%;
  display: flex;
  flex-direction: column;
`;

const Header = styled.div``;

const Footer = styled.div``;

const Content = styled.div<{ topMargin: number }>`
  overflow-y: auto;
  overflow-x: hidden;
  padding-right: 7px;
  margin-top: ${(props) => props.topMargin}px;

  ::-webkit-scrollbar {
    width: 10px;
  }
  ::-webkit-scrollbar-track {
    background: ${(props) => props.theme.colors.lighter};
  }
  ::-webkit-scrollbar-thumb {
    background: ${(props) => props.theme.colors.light};
  }
  ::-webkit-scrollbar-thumb:hover {
    background: #4c4c4c;
  }
`;

function getMaxHeight(
  container: HTMLDivElement,
  header: HTMLDivElement,
  footer: HTMLDivElement,
  contentMargin: number
): string {
  const hh = header.getBoundingClientRect().height;
  const ch = container.getBoundingClientRect().height;
  const fh = footer.getBoundingClientRect().height;
  const margin = contentMargin >= 0 ? contentMargin : 0;
  return `${ch - hh - fh - margin}px`;
}

interface ScrollableContainerProps {
  header: React.ReactNode;
  footer?: React.ReactNode;
  height: number;
  contentTopMargin: number;
  children: React.ReactNode;
}

export default function ScrollableContainer({
  header,
  footer,
  height,
  contentTopMargin,
  children,
}: ScrollableContainerProps): JSX.Element {
  const headerRef = useRef<HTMLDivElement>(null);
  const contentRef = useRef<HTMLDivElement>(null);
  const containerRef = useRef<HTMLDivElement>(null);
  const footerRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    if (headerRef.current && containerRef.current && footerRef.current) {
      if (contentRef.current) {
        const heightVal = getMaxHeight(
          containerRef.current,
          headerRef.current,
          footerRef.current,
          contentTopMargin
        );
        contentRef.current.style.maxHeight = heightVal;
      }
    }
  }, [headerRef.current && containerRef.current && footerRef.current]);

  return (
    <Container height={height} ref={containerRef}>
      <Header ref={headerRef}>{header}</Header>
      <Content ref={contentRef} topMargin={contentTopMargin}>
        {children}
      </Content>
      <Footer ref={footerRef}>{footer}</Footer>
    </Container>
  );
}
