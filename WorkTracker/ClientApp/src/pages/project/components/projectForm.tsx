import React, { useEffect } from "react";
import { useProject } from "../hooks";
import { Project } from "../../../types";
import {
  Button,
  GenericContainer,
  Text,
  TextFieldInput,
} from "../../../components";
import "./components.scss";

const FormContainer = GenericContainer("display-flex flex-column");
const Content = GenericContainer();
const ViewContent = GenericContainer("margin-left-right");
const Footer = GenericContainer("margin-left-right display-flex flex-row");

interface ProjectFormProps {
  initialValues: Project;
  onCancel: () => void;
}
export function ProjectForm({ initialValues, onCancel }
                              : ProjectFormProps): JSX.Element {
  const {
    bindings: { name, description },
    createdAt,
    exists,
    editable,
    submit,
    delProject,
    comProject,
    setEditable,
    setProject,
  } = useProject(initialValues);

  useEffect(() => {
    setProject(initialValues);
  }, [initialValues]);

  function onSubmit() {
    submit();
    cancel();
  }
  function cancel() {
    if (exists && editable) {
      setEditable(false);
    } else {
      onCancel();
    }
  }
  async function onDelete() {
    await delProject();
    onCancel();
  }
  async function onComplete() {
    await comProject();
    onCancel();
  }

  if (exists) {
    if (editable) {
      return (
        <FormContainer>
          <Text bold margin={"0 0 0 5px"} value={"Edit Project"} />
          <Content>
            <TextFieldInput {...name} />
            <TextFieldInput {...description} />
            <Text bold margin={"10px 0 0 5px"} value={"Creation date"} />
            <Text margin={"0 0 12px 5px"} value={createdAt} />
          </Content>
          <Footer>
            <div className={"flex-1"}>
              <Button isDeleteButton onClick={onDelete}>Delete</Button>
            </div>
            <Button primary onClick={onSubmit}>Save</Button>
            <div style={{ marginRight: "10px" }}> </div>
            <Button onClick={cancel}>Cancel</Button>
          </Footer>
        </FormContainer>
      );
    }
    return (
      <FormContainer>
        <ViewContent>
          <Text bold value={"Project name"} />
          <Text value={name.value} />
          <Text bold margin={"10px 0 0 0"} value={"Description"} />
          <Text value={description.value} />
          <Text bold margin={"10px 0 0 0"} value={"Creation date"} />
          <Text value={createdAt} />
        </ViewContent>
        <Footer style={{ marginTop: "10px" }}>
          <div className={"flex-1"}>
            <Button isDeleteButton onClick={onDelete}>Delete</Button>
          </div>
          <Button primary onClick={onComplete}>Complete</Button>
          <div style={{ marginRight: "10px" }}> </div>
          <Button primary onClick={() => setEditable(true)}>Edit</Button>
          <div style={{ marginRight: "10px" }}> </div>
          <Button onClick={cancel}>Cancel</Button>
        </Footer>
      </FormContainer>
    );
  } else {
    return (
      <FormContainer>
        <Text bold margin={"0 0 0 5px"} value={"Create new Project"} />
        <Content>
          <TextFieldInput
            {...name}
            placeholder={"Name for the project"}
          />
          <TextFieldInput
            {...description}
            placeholder={"Description of the project"}
          />
        </Content>
        <Footer>
          <div className={"flex-1"}> </div>
          <Button primary onClick={onSubmit}>Create</Button>
          <div style={{ marginRight: "10px" }}> </div>
          <Button onClick={cancel}>Cancel</Button>
        </Footer>
      </FormContainer>
    );
  }
}
