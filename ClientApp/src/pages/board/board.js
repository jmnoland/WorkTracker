import React, { useContext, useEffect, useState } from "react";
import styled from "styled-components";
import { UserDetailContext } from "../../context/userDetails";
import { CreateStoryModal } from "./components/createStoryModal";
import {
  CreateStory,
  GetStories,
  OrderUpdate,
  ChangeState,
} from "../../services/story";
import { StateColumn } from "./components/StateColumn";
import { DragDropContext } from "react-beautiful-dnd";

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

  const createPayload = (stateId, storyList) => {
    const temp = storyList.reduce((total, story, index) => {
      total[story.storyId] = index;
      return total;
    }, {});
    return { stateId: Number(stateId), stories: temp };
  };

  const move = (source, destination, droppableSource, droppableDestination) => {
    const sourceClone = Array.from(source);
    const destClone = Array.from(destination);
    const [removed] = sourceClone.splice(droppableSource.index, 1);

    destClone.splice(droppableDestination.index, 0, removed);

    const result = {};
    result[droppableSource.droppableId] = sourceClone;
    result[droppableDestination.droppableId] = destClone;

    return result;
  };

  const onDragEnd = async (result) => {
    const { source, destination } = result;
    if (!destination) {
      return;
    }
    if (source.droppableId === destination.droppableId) {
      const stateId = Number(source.droppableId);
      const items = reorder(stories[stateId], source.index, destination.index);
      setStories({ ...stories, [stateId]: items });
      await OrderUpdate(createPayload(stateId, items));
    } else {
      const storyId = Number(stories[source.droppableId][source.index].storyId);
      const items = move(
        stories[source.droppableId],
        stories[destination.droppableId],
        source,
        destination
      );
      setStories({ ...stories, ...items });
      await ChangeState(
        storyId,
        createPayload(destination.droppableId, items[destination.droppableId])
      );
      await OrderUpdate(
        createPayload(source.droppableId, items[source.droppableId])
      );
    }
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
        <DragDropContext onDragEnd={(r) => onDragEnd(r)}>
          {userDetail.states.map((state) => (
            <StateColumn
              key={state.stateId}
              state={state}
              stories={stories && stories[state.stateId]}
              createNew={createNew}
              onDragEnd={onDragEnd}
            />
          ))}
        </DragDropContext>
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
