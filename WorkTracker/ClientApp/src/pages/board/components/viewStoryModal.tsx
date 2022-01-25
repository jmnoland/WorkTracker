import React, { useState, useEffect } from "react";
import { useForm } from "../../../helper";
import {
  Modal,
  Button,
  EditableText,
  GenericContainer,
  ScrollableContainer,
} from "../../../components";
import { State, Task, Story } from "../../../types";
import fields from "../fields";
import { createNewTask, handleTaskChange, parseTasks } from "../functions";
import "./components.scss";
import { useAppDispatch, useAppSelector } from "../../../redux/hooks";
import { getStoryTasks, deleteTask } from "../../../redux/actions";

const Content = GenericContainer();
const Description = GenericContainer("flex-1");
const Footer = GenericContainer("display-flex");
const TaskInputContainer = GenericContainer("hide-overflow flex-1");
const Row = GenericContainer("display-flex height-40");
const TaskHeader = GenericContainer("display-flex");
const SVG = ({
  children,
  onClick,
} : { children: React.ReactNode, onClick: React.MouseEventHandler<SVGSVGElement> }
) => (<svg onClick={onClick} className="delete-svg">{children}</svg>);

interface ViewStoryModalProps {
  initialValues: Story;
  userStates?: State[];
  openModal: boolean;
  deleteStory: (storyId: number, stateId: number) => void;
  onCancel: () => void;
  onSave: (
    storyId: number,
    listOrder: number,
    title: string,
    description: string,
    stateId: number,
    tasks: Task[]
  ) => void;
}

export function ViewStoryModal({
  initialValues,
  openModal,
  deleteStory,
  onCancel,
  onSave,
}: ViewStoryModalProps): JSX.Element {
  const allTasks = useAppSelector((state) => state.story.tasks);
  const [tasks, setTasks] = useState(
    (initialValues && initialValues.tasks) || []
  );
  const [loading, setLoading] = useState(false);
  const [deleteLoading, setDeleteLoading] = useState(false);
  const [taskCount, setTaskCount] = useState(0);
  const dispatch = useAppDispatch();

  useEffect(() => {
    if (initialValues.storyId !== undefined) {
      if (allTasks[initialValues.storyId]) setTasks(allTasks[initialValues.storyId]);
      else if (!initialValues.tasks) {
        dispatch(getStoryTasks({ storyId: initialValues.storyId }) as any);
      }
    }
  }, [allTasks]);

  const obj = useForm(
    fields,
    initialValues
  );
  const { title, description, storyId, listOrder, stateId } = obj.form;

  const addTask = () => {
    setTasks([
      ...tasks,
      createNewTask(taskCount, storyId.value),
    ]);
    setTaskCount(taskCount + 1);
  };

  const onDelete = async () => {
    setDeleteLoading(true);
    try {
      await deleteStory(storyId.value, stateId.value);
      obj.reset();
      onCancel();
    } catch {
      setDeleteLoading(false);
    }
  };

  const removeTask = async (taskId: number) => {
    const { storyId } = initialValues;
    const taskToRemove = tasks.filter((task) => task.taskId === taskId)[0];
    if (taskToRemove && !taskToRemove.new && storyId)
      dispatch(deleteTask({ storyId, taskId }) as any);
    setTasks([...tasks.filter((task) => task.taskId !== taskId)]);
  };

  const handleChange = (taskId: number, value: string) => {
    const items = handleTaskChange(tasks, taskId, value);
    setTasks(items);
  };

  const handleSubmit = async () => {
    setLoading(true);
    const finalTasks = parseTasks(tasks);
    try {
      await onSave(
        storyId.value,
        listOrder.value,
        title.value,
        description.value,
        stateId.value,
        finalTasks
      );
      obj.reset();
      onCancel();
    } catch {
      setLoading(false);
    }
  };

  const footerContent = (
    <Footer>
      <Button onClick={onCancel}>
        Cancel
      </Button>
      <div style={{ marginRight: "10px" }}> </div>
      <Button primary onClick={handleSubmit} loading={loading}>
        Save
      </Button>
    </Footer>
  );

  const footerLeft = (
    <Button isDeleteButton onClick={onDelete} loading={deleteLoading}>
      Delete
    </Button>
  );

  const modalContent = (
    <>
      <EditableText
        type={"area"}
        height={"150px"}
        useBackground
        {...description}
      />
      <ScrollableContainer
        height={260}
        contentTopMargin={20}
        header={
          <TaskHeader>
            <Description>Add tasks to this story.</Description>
            <Button primary isSmallButton onClick={addTask}>
              Add
            </Button>
          </TaskHeader>
        }
      >
        {tasks.map((task) => (
          <Row key={task.taskId}>
            <TaskInputContainer>
              <EditableText
                edit={task.new}
                height={"30px"}
                margin={"10px"}
                onChange={(e: string) => handleChange(task.taskId, e)}
                value={task.description}
              />
            </TaskInputContainer>
            <SVG onClick={() => removeTask(task.taskId)}>
              <path d="M 15 4 C 14.476563 4 13.941406 4.183594 13.5625 4.5625 C 13.183594 4.941406 13 5.476563 13 6 L 13 7 L 7 7 L 7 9 L 8 9 L 8 25 C 8 26.644531 9.355469 28 11 28 L 23 28 C 24.644531 28 26 26.644531 26 25 L 26 9 L 27 9 L 27 7 L 21 7 L 21 6 C 21 5.476563 20.816406 4.941406 20.4375 4.5625 C 20.058594 4.183594 19.523438 4 19 4 Z M 15 6 L 19 6 L 19 7 L 15 7 Z M 10 9 L 24 9 L 24 25 C 24 25.554688 23.554688 26 23 26 L 11 26 C 10.445313 26 10 25.554688 10 25 Z M 12 12 L 12 23 L 14 23 L 14 12 Z M 16 12 L 16 23 L 18 23 L 18 12 Z M 20 12 L 20 23 L 22 23 L 22 12 Z" />
            </SVG>
          </Row>
        ))}
      </ScrollableContainer>
    </>
  );

  return (
    <Modal
      title={
        <span>
          <EditableText height={"50px"} {...title} />
        </span>
      }
      visible={openModal}
      onClose={onCancel}
      footer={footerContent}
      footerLeft={footerLeft}
    >
      <Content>{modalContent}</Content>
    </Modal>
  );
}
