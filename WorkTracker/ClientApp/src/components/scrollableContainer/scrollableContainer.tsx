import React, { useRef, useEffect } from "react";
import styled from "styled-components";
import "./scrollableContainer.scss";

const Container = styled.div<{ height: number }>`
  min-height: ${(props) => props.height}px;
`;

const Header = styled.div``;

const Footer = styled.div``;

const Content = styled.div<{ topMargin: number }>`
  margin-top: ${(props) => props.topMargin}px;
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
    <Container className="scrollable-container" height={height} ref={containerRef}>
      <Header ref={headerRef}>{header}</Header>
      <Content className="scrollable-content" ref={contentRef} topMargin={contentTopMargin}>
        {children}
      </Content>
      <Footer ref={footerRef}>{footer}</Footer>
    </Container>
  );
}
