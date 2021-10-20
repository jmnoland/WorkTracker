import React, { useContext, useEffect, useState } from "react";
import { UserDetailContext } from "../../context/userDetails";
import { NotificationContext } from "../../context/notification";
import { Notification, GenericContainer } from "../../components";
import { CreateStoryModal } from "./components/createStoryModal";
import { ViewStoryModal } from "./components/viewStoryModal";
import {
  CreateStory,
  UpdateStory,
  DeleteStory,
  GetStories,
  OrderUpdate,
  ChangeState,
} from "../../services/story";
import { StateColumn } from "./components/StateColumn";
import { DragDropContext, DropResult } from "react-beautiful-dnd";
import {
  Dictionary,
  Story,
  Task,
  DroppableType,
} from "../../types";
import { createPayload, reorder } from "./functions";
import "./board.scss";

const BoardContainer = GenericContainer("board-container");

export default function Board(): JSX.Element {
  const { userDetail } = useContext(UserDetailContext);
  const { setContent } = useContext(NotificationContext);

  const [openCreateModal, setOpenCreateModal] = useState(false);
  const [openViewModal, setOpenViewModal] = useState(false);
  const [viewStory, setViewStory] = useState<Story>({});
  const [storyState, setStoryState] = useState<number | undefined>(undefined);
  const [stories, setStories] = useState<Dictionary<Story[]>>();

  async function getStateStories(stories: Dictionary<Story[]>, stateId: number, save: boolean) {
    stories[stateId] = await GetStories(stateId);
    if (save) setStories(stories);
  }

  useEffect(() => {
    async function fetchData() {
      const temp: Dictionary<Story[]> = {};
      let count = 0;
      userDetail?.states.forEach(async (state) => {
        temp[state.stateId] = await GetStories(state.stateId);
        count += 1;
        if (count === userDetail.states.length) {
          setStories(temp);
        }
      });
    }
    if (userDetail?.states) {
      fetchData();
    }
  }, [userDetail?.states]);

  if (!userDetail?.states) {
    return <></>;
  }

  const move = (
    source: Story[],
    destination: Story[],
    droppableSource: DroppableType,
    droppableDestination: DroppableType
  ) => {
    const sourceClone = Array.from(source);
    const destClone = Array.from(destination);
    const [removed] = sourceClone.splice(droppableSource.index, 1);
    removed.stateId = parseInt(droppableDestination.droppableId);

    destClone.splice(droppableDestination.index, 0, removed);

    const result = {} as Dictionary<Story[]>;
    result[droppableSource.droppableId] = sourceClone;
    result[droppableDestination.droppableId] = destClone;
    return result;
  };

  const onDragEnd = async (result: DropResult) => {
    const { source, destination } = result;
    if (!destination || stories === undefined) {
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

  const onCreateSave = async (
    title: string,
    description: string,
    state: number,
    tasks: Task[],
    storyPosition: number,
  ) => {
    await CreateStory(title, description, storyPosition, state, tasks);
    if (stories) await getStateStories(stories, state, true);
    setStoryState(undefined);
    setContent("New story created");
  };

  const onEditSave = async (
    storyId: number,
    listOrder: number,
    title: string,
    description: string,
    state: number,
    tasks: Task[],
  ) => {
    await UpdateStory(storyId, listOrder, title, description, state, tasks);
    if (stories) await getStateStories(stories, state, true);
    setViewStory({});
    setContent("Story updated");
  };

  const onDelete = async (deleteFunc: (id: number) => void, id: number, state: number) => {
    await deleteFunc(id);
    if (stories) await getStateStories(stories, state, true);
    setViewStory({});
    setContent("Story deleted");
  };

  const createNew = (stateId: number) => {
    setStoryState(stateId);
    setOpenCreateModal(true);
  };

  const setViewValues = (story: Story) => {
    setViewStory(story);
    setOpenViewModal(true);
  };

  return (
    <>
      <BoardContainer>
        <Notification />
        <DragDropContext onDragEnd={(r: DropResult) => onDragEnd(r)}>
          {userDetail.states.map((state) => (
            <StateColumn
              key={state.stateId}
              state={state}
              stories={stories && stories[state.stateId]}
              createNew={createNew}
              viewEdit={setViewValues}
              onDragEnd={onDragEnd}
            />
          ))}
        </DragDropContext>
      </BoardContainer>
      {openViewModal && (
        <ViewStoryModal
          initialValues={viewStory}
          openModal={openViewModal}
          deleteStory={(id: number, state: number) => onDelete(DeleteStory, id, state)}
          onSave={onEditSave}
          onCancel={() => setOpenViewModal(false)}
        />
      )}
      {openCreateModal && (
        <CreateStoryModal
          defaultState={storyState}
          openModal={openCreateModal}
          storyPosition={
            stories && storyState ? stories[storyState] && stories[storyState].length : undefined
          }
          onSave={onCreateSave}
          onCancel={() => setOpenCreateModal(false)}
        />
      )}
    </>
  );
}
