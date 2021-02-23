import React, { useContext, useEffect, useState } from "react";
import styled from "styled-components";
import { UserDetailContext } from "../../context/userDetails";
import { CreateStoryModal } from "./components/createStoryModal";
import { DragDropContext, Droppable, Draggable } from "react-beautiful-dnd";
import { CreateStory, GetStories } from "../../services/story";

const BoardContainer = styled.div`
  height: inherit;
  margin-top: 40px;
  display: flex;
  justify-content: space-around;
  overflow-x: auto;
`;

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

const StateContent = styled.div``;

const StateButton = styled.button``;

const StoryContainer = styled.div`
  cursor: pointer;
  width: 100%;
  height: 70px;
  padding: 10px 10px 10px 0px;
  box-shadow: 0px 0px 5px 1px ${(props) => props.theme.colors.dark};
  border-top: 2px solid ${(props) => props.theme.colors.dark};
  margin-top: 10px;
`;

export default function Board() {
  const { userDetail } = useContext(UserDetailContext);
  const [openModal, setOpenModal] = useState(false);
  const [storyState, setStoryState] = useState(null);
  const [stories, setStories] = useState();

  useEffect(() => {
    async function fetchData() {
      const temp = {};
      let count = 0;
      userDetail.states.forEach(async (state) => {
        temp[state.stateId] = await GetStories(state.stateId);
        count += 1;
        if (count === userDetail.states.length) {
          setStories(temp);
        }
      });
    }
    if (userDetail.states) {
      fetchData();
    }
  }, [userDetail.states]);

  if (!userDetail.states) {
    return null;
  }

  const onSave = async (title, description, state, tasks) => {
    await CreateStory(title, description, state, tasks);
    setOpenModal(false);
    setStoryState(null);
  };

  const createNew = (stateId) => {
    setStoryState(stateId);
    setOpenModal(true);
  };

  return (
    <>
      <BoardContainer>
        {userDetail.states.map((state) => (
          <StateContainer key={state.stateId}>
            <StateHeader>
              <span>{state.name}</span>
              <StateButton onClick={() => createNew(state.stateId)}>
                Add
              </StateButton>
            </StateHeader>
            <DragDropContext>
              <Droppable droppableId="droppable">
                {(provided) => (
                  <div {...provided.droppableProps} ref={provided.innerRef}>
                    {stories &&
                      stories[state.stateId] &&
                      stories[state.stateId].map((story, index) => (
                        <Draggable
                          key={`story-${story.storyId}`}
                          draggableId={`story-${story.storyId}`}
                          index={index}
                        >
                          {(provided) => (
                            <div
                              ref={provided.innerRef}
                              {...provided.draggableProps}
                              {...provided.dragHandleProps}
                            >
                              <StoryContainer>
                                <div>{story.title}</div>
                                <div>{story.description}</div>
                              </StoryContainer>
                            </div>
                          )}
                        </Draggable>
                      ))}
                  </div>
                )}
              </Droppable>
            </DragDropContext>
          </StateContainer>
        ))}
      </BoardContainer>
      <CreateStoryModal
        defaultState={storyState}
        userStates={userDetail.states}
        openModal={openModal}
        onSave={onSave}
        onCancel={() => setOpenModal(false)}
      />
    </>
  );
}
