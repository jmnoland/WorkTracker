import React, { useState } from "react";
import styled from "styled-components";
import { useObject } from "../../../helper";
import { Modal, Button, EditableText } from "../../../components";

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
  onCancel,
  onSave,
}) {
  const [tasks, setTasks] = useState(initialValues.tasks);
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
    },
    initialValues
  );
  const { title, description } = fields.data;

  const addTask = () => {
    setTasks([
      ...tasks,
      { taskId: tasks.length + 1, storyId: 0, description: "", complete: 0 },
    ]);
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
    await onSave(title.value, description.value, defaultState, tasks);
    setLoading(false);
  };

  const footerContent = (
    <Footer>
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
            <EditableText
              height={"30px"}
              onChange={(e) => handleChange(task.taskId, e)}
              value={task.description}
            />
            <div onClick={() => removeTask(task.taskId)}>Remove</div>
          </div>
        ))}
        <Button primary onClick={addTask}>
          Save
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
