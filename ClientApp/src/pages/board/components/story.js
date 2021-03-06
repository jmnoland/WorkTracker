import React from "react";
import styled from "styled-components";

const StoryContainer = styled.div`
  cursor: pointer;
  width: 100%;
  height: 70px;
  padding: 10px 10px 10px 10px;
  box-shadow: 0px 0px 5px 1px ${(props) => props.theme.colors.dark};
  border-top: 2px solid ${(props) => props.theme.colors.dark};
  background: ${(props) => props.theme.colors.background};
`;

const Row = styled.div``;

const Col = styled.div``;

export function Story({ title, createdBy, updatedAt }) {
  return (
    <StoryContainer>
      <div>{title}</div>
      <Row>
        <Col>
          <div>{`Created by: ${createdBy}`}</div>
        </Col>
        {updatedAt && (
          <Col>
            <div>{`Updated at: ${updatedAt}`}</div>
          </Col>
        )}
      </Row>
    </StoryContainer>
  );
}
