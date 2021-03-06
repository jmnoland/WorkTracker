import React, { useContext } from "react";
import styled from "styled-components";
import { Droppable, Draggable } from "react-beautiful-dnd";
import { Story } from "./story";
import { UserDetailContext } from "../../../context/userDetails";
import { getUserMapping, parseDateTime } from "../../../helper";

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

export function StateColumn({ state, stories, createNew }) {
  const {
    userDetail: { users },
  } = useContext(UserDetailContext);
  const { stateId, name } = state;
  const userMapping = getUserMapping(users);

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
                        createdBy={userMapping[story.createdBy]}
                        updatedAt={
                          story.modifiedAt
                            ? parseDateTime(story.modifiedAt)
                            : null
                        }
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
