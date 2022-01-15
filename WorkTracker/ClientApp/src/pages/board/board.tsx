import React, { useContext, useEffect, useState } from "react";
import { NotificationContext } from "../../context/notification";
import { Notification, GenericContainer } from "../../components";
import { CreateStoryModal } from "./components/createStoryModal";
import { ViewStoryModal } from "./components/viewStoryModal";
import { StateColumn } from "./components/StateColumn";
import { DragDropContext, DropResult } from "react-beautiful-dnd";
import {
  Story,
  Task,
  DroppableType,
} from "../../types";
import { createPayload, reorder } from "./functions";
import { useAppDispatch, useAppSelector } from "../../redux/hooks";
import "./board.scss";
import {
  changeState,
  createStory,
  deleteStory, getStories,
  orderUpdate,
  updateStory
} from "../../redux/actions";

const BoardContainer = GenericContainer("board-container");

export default function Board(): JSX.Element {
  const userDetail = useAppSelector((state) => state.user.userDetail);
  const { stories, tasks } = useAppSelector((state) => state.story);
  const dispatch = useAppDispatch();
  const { setContent } = useContext(NotificationContext);

  const [openCreateModal, setOpenCreateModal] = useState(false);
  const [openViewModal, setOpenViewModal] = useState(false);
  const [viewStory, setViewStory] = useState<Story>({});
  const [storyState, setStoryState] = useState<number | undefined>(undefined);

  useEffect(() => {
    if (userDetail?.states) {
      userDetail?.states.forEach((state) => {
        dispatch(getStories({ stateId: state.stateId }) as any);
      });
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

    const result: Record<string, Story[]> = {};
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
      dispatch(orderUpdate(createPayload(stateId, items)) as any);
    } else {
      const sourceId = parseInt(source.droppableId);
      const destinationId = parseInt(destination.droppableId);
      const storyId = Number(stories[sourceId][source.index].storyId);
      const items = move(
        stories[sourceId],
        stories[destinationId],
        source,
        destination
      );
      dispatch(changeState({
        currentStateId: sourceId,
        storyId: storyId,
        ...createPayload(
          destination.droppableId,
          items[destination.droppableId]
        ),
      }) as any)
      dispatch(orderUpdate(
          createPayload(source.droppableId, items[source.droppableId])
      ) as any);
    }
  };

  const onCreateSave = async (
    title: string,
    description: string,
    state: number,
    tasks: Task[],
    storyPosition: number,
  ) => {
    dispatch(createStory({
      title,
      description,
      storyPosition,
      state,
      tasks,
    }) as any);
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
    dispatch(updateStory({
      storyId,
      listOrder,
      title,
      description,
      state,
      tasks,
    }) as any);
    setViewStory({});
    setContent("Story updated");
  };

  const onDelete = (id: number) => {
    dispatch(deleteStory({
      storyId: id,
    }) as any);
    setViewStory({});
    setContent("Story deleted");
  };

  const createNew = (stateId: number) => {
    setStoryState(stateId);
    setOpenCreateModal(true);
  };

  const setViewValues = (story: Story) => {
    if (story.storyId) {
      story.tasks = tasks[story.storyId] ? tasks[story.storyId] : [];
    }
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
          deleteStory={(id: number) => onDelete(id)}
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
