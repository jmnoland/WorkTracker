import React from "react";
import styled from "styled-components";
import { Story } from "./story";
import { DragDropContext, Droppable, Draggable } from "react-beautiful-dnd";

const StateContainer = styled.div`
  box-shadow: 0px 0px 5px 2px ${(props) => props.theme.colors.dark};
  border-top: 2px solid ${(props) => props.theme.colors.dark};
  padding: 10px;
  width: 100%;
  padding: ${(props) => props.theme.padding.medium};
  width: 100%;
`;

const StateHeader = styled.div`
  margin-bottom: ${(props) => props.theme.padding.large};
`;

const StateButton = styled.button``;

export function StateColumn({ state, stories, createNew, onDragEnd }) {
  return (
    <StateContainer>
      <StateHeader>
        <span>{state.name}</span>
        <StateButton onClick={() => createNew(state.stateId)}>Add</StateButton>
      </StateHeader>
      <DragDropContext onDragEnd={(r) => onDragEnd(r, state.stateId)}>
        <Droppable droppableId="droppable">
          {(provided) => (
            <div
              {...provided.droppableProps}
              style={{ minHeight: "50%" }}
              ref={provided.innerRef}
            >
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
            </div>
          )}
        </Droppable>
      </DragDropContext>
    </StateContainer>
  );
}
