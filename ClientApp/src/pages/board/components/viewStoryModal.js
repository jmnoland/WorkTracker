import React, { useState, useEffect } from "react";
import styled from "styled-components";
import { useObject } from "../../../helper";
import {
  Modal,
  Button,
  EditableText,
  TextFieldInput,
  ScrollableContainer,
} from "../../../components";
import { GetStoryTasks, DeleteTask } from "../../../services/story";

const Content = styled.div``;

const Description = styled.div`
  flex: 1;
`;

const Footer = styled.div`
  display: flex;
`;

const TaskInputContainer = styled.div`
  flex: 1;
`;

const Row = styled.div`
  display: flex;
  height: 40px;
`;

const SVG = styled.svg`
  width: 40px;
  fill: ${(props) => props.theme.colors.white};
  cursor: pointer;
  margin-top: 4px;
  &:hover {
    fill: ${(props) => props.theme.colors.danger};
  }
`;

const TaskHeader = styled.div`
  display: flex;
`;

export function ViewStoryModal({
  initialValues,
  userStates,
  openModal,
  deleteStory,
  onCancel,
  onSave,
}) {
  const [tasks, setTasks] = useState(initialValues.tasks || []);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    async function fetchData() {
      const data = await GetStoryTasks(initialValues.storyId);
      setTasks(data);
    }
    fetchData();
  }, []);

  const fields = useObject(
    {
      title: {
        name: "title",
        value: "",
        validation: {
          rules: [
            {
              validate: (value) => {
                return value !== "" && value;
              },
              message: "Please enter a title",
            },
          ],
        },
      },
      description: {
        name: "description",
        value: "",
        validation: {
          rules: [],
        },
      },
      storyId: {},
      listOrder: {},
      stateId: {},
    },
    initialValues
  );
  const { title, description, storyId, listOrder, stateId } = fields.data;

  const addTask = () => {
    setTasks([
      ...tasks,
      {
        taskId: tasks.length + 1,
        storyId: storyId.value,
        description: "",
        complete: false,
        new: true,
      },
    ]);
  };

  const onDelete = async () => {
    setLoading(true);
    await deleteStory(storyId.value, stateId.value);
  };

  const removeTask = async (taskId) => {
    const taskToRemove = tasks.filter((task) => task.taskId === taskId);
    if (taskToRemove && !taskToRemove.new) await DeleteTask(taskId);
    setTasks([...tasks.filter((task) => task.taskId !== taskId)]);
  };

  const handleChange = (taskId, value) => {
    const temp = tasks.find((task) => task.taskId === taskId);
    const items = tasks.reduce((total, task) => {
      if (task.taskId !== taskId) total.push(task);
      else total.push({ ...temp, description: value });
      return total;
    }, []);
    setTasks(items);
  };

  const handleSubmit = async () => {
    setLoading(true);
    await onSave(
      storyId.value,
      listOrder.value,
      title.value,
      description.value,
      stateId.value,
      tasks
    );
  };

  const footerContent = (
    <Footer>
      <Button onClick={onDelete}>Delete</Button>
      <Button secondary>Cancel</Button>
      <Button primary onClick={handleSubmit} loading={loading}>
        Save
      </Button>
    </Footer>
  );

  const modalContent = (
    <>
      <EditableText {...title} />
      <EditableText {...description} height={"150px"} />
      <ScrollableContainer
        height={170}
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
              <TextFieldInput
                height={"30px"}
                onChange={(e) => handleChange(task.taskId, e)}
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
    <Modal visible={openModal} onClose={onCancel} footer={footerContent}>
      <Content>{modalContent}</Content>
    </Modal>
  );
}
