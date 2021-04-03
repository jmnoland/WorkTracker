import React, { useRef, useState, useEffect, useContext } from "react";
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
  display: flex;
  flex-direction: column;
`;

const StateHeader = styled.div`
  margin-bottom: ${(props) => props.theme.padding.large};
`;

const StateFooter = styled.div``;

const Content = styled.div`
  overflow-y: auto;
  overflow-x: hidden;
  padding-right: 7px;

  ::-webkit-scrollbar {
    width: 10px;
  }
  ::-webkit-scrollbar-track {
    background: ${(props) => props.theme.colors.light};
  }
  ::-webkit-scrollbar-thumb {
    background: #555555;
  }
  ::-webkit-scrollbar-thumb:hover {
    background: #4c4c4c;
  }
`;

const DropContainer = styled.div`
  display: flex;
  flex-direction: column;
`;

const StateButton = styled.button`
  float: right;
`;

function getMaxHeight(container, header, footer) {
  const hh = header.getBoundingClientRect().height;
  const ch = container.getBoundingClientRect().height;
  const fh = footer.getBoundingClientRect().height;
  // element heights - 10 padding 20 margin
  return `${ch - hh - fh - 10 - 20}px`;
}

export function StateColumn({ state, stories, viewEdit, createNew }) {
  const {
    userDetail: { users },
  } = useContext(UserDetailContext);
  const headerRef = useRef();
  const contentRef = useRef();
  const containerRef = useRef();
  const footerRef = useRef();
  const [height, setHeight] = useState(-1);
  const droppableStyling = height !== -1 ? { minHeight: height } : {};

  useEffect(() => {
    if (!stories) return;
    if (headerRef.current && containerRef.current && footerRef.current) {
      if (contentRef.current) {
        const heightVal = getMaxHeight(
          containerRef.current,
          headerRef.current,
          footerRef.current
        );
        contentRef.current.style.maxHeight = heightVal;
        setHeight(heightVal);
      }
    }
  }, [stories]);

  const { stateId, name } = state;
  const userMapping = getUserMapping(users);

  return (
    <StateContainer ref={containerRef}>
      <StateHeader ref={headerRef}>
        <span>{name}</span>
        <StateButton onClick={() => createNew(stateId)}>Add</StateButton>
      </StateHeader>
      <Content ref={contentRef}>
        <Droppable droppableId={stateId.toString()}>
          {(provided) => (
            <DropContainer
              style={droppableStyling}
              {...provided.droppableProps}
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
                        onClick={() => viewEdit(story)}
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
      </Content>
      <StateFooter ref={footerRef} />
    </StateContainer>
  );
}
