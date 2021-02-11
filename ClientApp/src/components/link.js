import React from "react";
import styled from "styled-components";

export default function BaseLink({ onClick, children }) {
  return <span onClick={onClick}>{children}</span>;
}
