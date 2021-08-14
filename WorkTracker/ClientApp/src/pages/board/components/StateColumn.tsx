import React, { useRef, useState, useEffect, useContext } from "react";
import styled from "styled-components";
import { Droppable, Draggable } from "react-beautiful-dnd";
import { Story } from "./story";
import { UserDetailContext } from "../../../context/userDetails";
import { Button } from "../../../components";
import { getUserMapping, parseDateTime } from "../../../helper";
import { State, Story as StoryType } from "../../../types";

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
    background: ${(props) => props.theme.colors.lighter};
  }
  ::-webkit-scrollbar-thumb {
    background: ${(props) => props.theme.colors.light};
  }
  ::-webkit-scrollbar-thumb:hover {
    background: #4c4c4c;
  }
`;

const DropContainer = styled.div`
  display: flex;
  flex-direction: column;
`;

const StateButton = styled.div`
  float: right;
`;

function getMaxHeight(
  container: HTMLDivElement,
  header: HTMLDivElement,
  footer: HTMLDivElement,
): string {
  const hh = header.getBoundingClientRect().height;
  const ch = container.getBoundingClientRect().height;
  const fh = footer.getBoundingClientRect().height;
  // element heights - 22 padding 20 margin
  return `${ch - hh - fh - 22 - 20}px`;
}

interface StateColumnProps {
  state: State;
  stories?: StoryType[];
  viewEdit: (story: StoryType) => void;
  createNew: (stateId: number) => void;
  onDragEnd: (result: any) => Promise<void>
}

export function StateColumn({
  state,
  stories,
  viewEdit,
  createNew,
}: StateColumnProps): JSX.Element {
  const {
    userDetail,
  } = useContext(UserDetailContext);
  const users = userDetail?.users;

  const headerRef = useRef<HTMLDivElement>(null);
  const contentRef = useRef<HTMLDivElement>(null);
  const containerRef = useRef<HTMLDivElement>(null);
  const footerRef = useRef<HTMLDivElement>(null);
  const [height, setHeight] = useState("");
  const droppableStyling = height !== "" ? { minHeight: height } : {};

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
        <StateButton>
          <Button isInactivePrimary onClick={() => createNew(stateId)}>
            Create
          </Button>
        </StateButton>
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
                          createdBy={story.createdBy ? userMapping[story.createdBy]: ""}
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
