import React, { useContext, useEffect, useState } from "react";
import styled from "styled-components";
import { UserDetailContext } from "../../context/userDetails";
import { CreateStoryModal } from "./components/createStoryModal";
import { DragDropContext, Droppable, Draggable } from "react-beautiful-dnd";
import { CreateStory, GetStories } from "../../services/story";

const BoardContainer = styled.div``;

const StateContainer = styled.div`
  display: flex;
`;
const StateHeader = styled.div``;

const StateContent = styled.div``;

const StateButton = styled.button``;

export default function Board() {
  const { userDetail } = useContext(UserDetailContext);
  const [openModal, setOpenModal] = useState(false);
  const [storyState, setStoryState] = useState(null);
  const [stories, setStories] = useState();

  useEffect(() => {
    async function fetchData() {
      const temp = {};
      userDetail.states.forEach(async (state, i) => {
        temp[state.stateId] = await GetStories(state.stateId);
        if (i === userDetail.states.length - 1) {
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
            <StateContent>
              {stories &&
                stories[state.stateId] &&
                stories[state.stateId].map((story) => (
                  <div key={story.storyId}>
                    <div>{story.title}</div>
                    <div>{story.description}</div>
                  </div>
                ))}
            </StateContent>
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
