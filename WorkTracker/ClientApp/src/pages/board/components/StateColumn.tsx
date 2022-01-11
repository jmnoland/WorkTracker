import React, { useRef, useState, useEffect } from "react";
import { Droppable, Draggable, DropResult } from "react-beautiful-dnd";
import { Story } from "./story";
import { Button, GenericContainer } from "../../../components";
import { getUserMapping, parseDateTime } from "../../../helper";
import { State, Story as StoryType } from "../../../types";
import { getMaxHeight } from "../functions";
import "./components.scss";
import { useAppSelector } from "../../../redux/hooks";

const StateButton = GenericContainer("float-right");
const StateFooter = GenericContainer("state-column-header");
const StateContainer = GenericContainer("state-column-container");
const StateHeader = GenericContainer("state-column-header");
const Content = GenericContainer("state-column-content");

interface StateColumnProps {
  state: State;
  stories?: StoryType[];
  viewEdit: (story: StoryType) => void;
  createNew: (stateId: number) => void;
  onDragEnd: (result: DropResult) => Promise<void>
}

export function StateColumn({
  state,
  stories,
  viewEdit,
  createNew,
}: StateColumnProps): JSX.Element {
  const userDetail = useAppSelector((state) => state.userDetail);
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
          footerRef.current,
          20
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
            <div className="display-flex flex-column"
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
            </div>
          )}
        </Droppable>
      </Content>
      <StateFooter ref={footerRef} />
    </StateContainer>
  );
}
