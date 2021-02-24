import React, { useContext, useEffect, useState } from "react";
import styled from "styled-components";
import { UserDetailContext } from "../../context/userDetails";
import { CreateStoryModal } from "./components/createStoryModal";
import { CreateStory, GetStories } from "../../services/story";
import { StateColumn } from "./components/StateColumn";

const BoardContainer = styled.div`
  height: inherit;
  margin-top: 40px;
  display: flex;
  justify-content: space-around;
  overflow-x: auto;
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

  const reorder = (list, startIndex, endIndex) => {
    const result = Array.from(list);
    const [removed] = result.splice(startIndex, 1);
    result.splice(endIndex, 0, removed);
    return result;
  };

  const onDragEnd = (result, stateId) => {
    if (!result.destination) {
      return;
    }
    const items = reorder(
      stories[stateId],
      result.source.index,
      result.destination.index
    );
    setStories({ ...stories, [stateId]: items });
  };

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
          <StateColumn
            key={state.stateId}
            state={state}
            stories={stories && stories[state.stateId]}
            createNew={createNew}
            onDragEnd={onDragEnd}
          />
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
