import React, { useContext, useEffect, useState } from "react";
import styled from "styled-components";
import { UserDetailContext } from "../../context/userDetails";
import { CreateStoryModal } from "./components/createStoryModal";
import { DragDropContext, Droppable, Draggable } from "react-beautiful-dnd";
import { CreateStory } from "../../services/story";

const BoardContainer = styled.div``;

const StateContainer = styled.div`
  display: flex;
`;

const StateButton = styled.button``;

export default function Board() {
  const { userDetail } = useContext(UserDetailContext);
  const [openModal, setOpenModal] = useState(true);
  const [storyState, setStoryState] = useState(null);
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
            <span>{state.name}</span>
            <StateButton onClick={() => createNew(state.stateId)}>
              Add
            </StateButton>
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
