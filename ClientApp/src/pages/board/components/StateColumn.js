import React from "react";
import styled from "styled-components";
import { Story } from "./story";
import { DragDropContext, Droppable, Draggable } from "react-beautiful-dnd";

const StateContainer = styled.div`
  box-shadow: 0px 0px 5px 2px ${(props) => props.theme.colors.dark};
  border-top: 2px solid ${(props) => props.theme.colors.dark};
  padding: ${(props) => props.theme.padding.medium};
  width: 100%;
`;

const StateHeader = styled.div`
  margin-bottom: ${(props) => props.theme.padding.large};
`;

const DropContainer = styled.div`
  display: flex;
  flex-direction: column;
`;

const StateButton = styled.button`
  float: right;
`;

export function StateColumn({ state, stories, createNew, onDragEnd }) {
  const { stateId, name } = state;

  return (
    <StateContainer>
      <StateHeader>
        <span>{name}</span>
        <StateButton onClick={() => createNew(stateId)}>Add</StateButton>
      </StateHeader>
      <Droppable droppableId={stateId.toString()}>
        {(provided) => (
          <DropContainer {...provided.droppableProps} ref={provided.innerRef}>
            {stories &&
              stories.map((story, index) => (
                <Draggable
                  key={`story-${story.storyId}`}
                  draggableId={`story-${story.storyId}`}
                  index={index}
                >
                  {(provided) => (
                    <div
                      ref={provided.innerRef}
                      style={{ marginTop: "10px" }}
                      {...provided.draggableProps}
                      {...provided.dragHandleProps}
                    >
                      <Story
                        title={story.title}
                        description={story.description}
                      />
                    </div>
                  )}
                </Draggable>
              ))}
          </DropContainer>
        )}
      </Droppable>
    </StateContainer>
  );
}
