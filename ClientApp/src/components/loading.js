import React from "react";
import styled from "styled-components";
import { theme } from "../constants/theme";

const Container = styled.div`
  width: max-content;
  margin-left: auto;
  margin-right: auto;
`;

function getSize(small) {
  const size = {};
  if (small) {
    size.width = "20px";
    size.height = "20px";
  } else {
    size.width = "64px";
    size.height = "64px";
  }
  return size;
}

export default function Loading({ small, primary }) {
  const size = getSize(small);
  return (
    <Container>
      <svg
        svg="http://www.w3.org/2000/svg"
        xmlns="http://www.w3.org/2000/svg"
        xlink="http://www.w3.org/1999/xlink"
        version="1.0"
        width={size.width}
        height={size.height}
        viewBox="0 0 128 128"
        space="preserve"
      >
        <g>
          <path
            d="M75.4 126.63a11.43 11.43 0 0 1-2.1-22.65 40.9 40.9 0 0 0 30.5-30.6 11.4 11.4 0 1 1 22.27 4.87h.02a63.77 63.77 0 0 1-47.8 48.05v-.02a11.38 11.38 0 0 1-2.93.37z"
            fill={primary ? theme.colors.white : theme.colors.dark}
          />
          <animateTransform
            attributeName="transform"
            type="rotate"
            from="0 64 64"
            to="360 64 64"
            dur="800ms"
            repeatCount="indefinite"
          ></animateTransform>
        </g>
      </svg>
    </Container>
  );
}
