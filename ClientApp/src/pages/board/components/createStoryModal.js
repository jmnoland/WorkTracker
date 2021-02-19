import React, { useState } from "react";
import styled from "styled-components";
import { useObject } from "../../../helper";
import { Modal, Button, Input, TextArea } from "../../../components";

const Content = styled.div``;

const TaskContainer = styled.div``;

const Description = styled.div``;

const Footer = styled.div`
  display: flex;
`;

export function CreateStoryModal({
  defaultState,
  userStates,
  openModal,
  onCancel,
  onSave,
}) {
  const initialValues = { title: "", description: "" };
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
  const footerContent = (
    <Footer>
      <Button secondary>Cancel</Button>
      <Button primary onClick={onSave}>
        Save
      </Button>
    </Footer>
  );

  return (
    <Modal
      title="Create story"
      visible={openModal}
      onClose={onCancel}
      footer={footerContent}
    >
      <Content>
        <Description>Fill in details below to create a new story.</Description>
        <Input label="Title" {...title} />
        <TextArea placeholder="Description" {...description} />
        <TaskContainer></TaskContainer>
      </Content>
    </Modal>
  );
}
