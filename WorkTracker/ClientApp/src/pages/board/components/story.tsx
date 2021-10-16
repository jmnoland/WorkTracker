import React from "react";
import { SubText, GenericContainer } from "../../../components";
import "./components.scss";

const StoryContainer = GenericContainer("story-container");

const Row = GenericContainer("display-flex story-row");

const Col = GenericContainer();

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
