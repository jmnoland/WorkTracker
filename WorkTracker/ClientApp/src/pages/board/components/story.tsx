import React from "react";
import styled from "styled-components";
import { SubText } from "../../../components";

const StoryContainer = styled.div`
  cursor: pointer;
  width: 100%;
  height: 70px;
  padding: 10px 10px 10px 10px;
  box-shadow: 0px 0px 5px 1px ${(props) => props.theme.colors.dark};
  border-top: 2px solid ${(props) => props.theme.colors.dark};
  background: ${(props) => props.theme.colors.background};

  &:hover {
    background: ${(props) => props.theme.colors.dark};
  }
`;

const Row = styled.div`
  display: flex;
  justify-content: space-between;
  margin-top: 6px;
`;

const Col = styled.div``;

interface StoryProps {
  title?: string;
  createdBy: string;
  updatedAt: string | null;
}

export function Story({
  title,
  createdBy,
  updatedAt
}: StoryProps): JSX.Element {
  return (
    <StoryContainer>
      <div>{title}</div>
      <Row>
        <Col>
          <SubText>{`Created by: ${createdBy}`}</SubText>
        </Col>
        {updatedAt && (
          <Col>
            <SubText>{`Updated at: ${updatedAt}`}</SubText>
          </Col>
        )}
      </Row>
    </StoryContainer>
  );
}
