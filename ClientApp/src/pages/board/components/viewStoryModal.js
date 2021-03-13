import React, { useState } from "react";
import styled from "styled-components";
import { useObject } from "../../../helper";
import {
  Modal,
  Button,
  EditableText,
  TextFieldInput,
  Icon,
} from "../../../components";
import { TrashIcon } from "../../../assets";

const Content = styled.div``;

const TaskContainer = styled.div``;

const Description = styled.div``;

const Footer = styled.div`
  display: flex;
`;

export function ViewStoryModal({
  initialValues,
  userStates,
  openModal,
  deleteStory,
  deleteTask,
  onCancel,
  onSave,
}) {
  const [tasks, setTasks] = useState(initialValues.tasks || []);
  const [loading, setLoading] = useState(false);
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
      },
    ]);
  };

  const onDelete = async () => {
    setLoading(true);
    await deleteStory(storyId.value, stateId.value);
  };

  const removeTask = (taskId) => {
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
      <TaskContainer>
        <Description>Tasks:</Description>
        {tasks.map((task) => (
          <div key={task.taskId}>
            <TextFieldInput
              height={"30px"}
              onChange={(e) => handleChange(task.taskId, e)}
              value={task.description}
            />
            <Icon
              onClick={() => removeTask(task.taskId)}
              src={TrashIcon}
            ></Icon>
          </div>
        ))}
        <Button primary onClick={addTask}>
          Add
        </Button>
      </TaskContainer>
    </>
  );

  return (
    <Modal visible={openModal} onClose={onCancel} footer={footerContent}>
      <Content>{modalContent}</Content>
    </Modal>
  );
}
