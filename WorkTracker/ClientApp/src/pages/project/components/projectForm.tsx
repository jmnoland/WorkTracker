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
const ContentDate = GenericContainer("display-flex");
const EditContentDate = GenericContainer("display-flex margin-left-right");
const FlexDiv = GenericContainer("flex-1");

interface ProjectFormProps {
  initialValues: Project;
  onCancel: () => void;
}
export function ProjectForm({ initialValues, onCancel }
                              : ProjectFormProps): JSX.Element {
  const {
    bindings: { name, description },
    createdAt,
    completedAt,
    isComplete,
    exists,
    editable,
    submit,
    delProject,
    comProject,
    setEditable,
    setProject,
    setInitialValues,
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
      setInitialValues();
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

  const completeContent = isComplete
    ? (<div>
      <Text bold margin={"10px 0 0 0"} value={"Completed at"} />
      <Text margin={"0 0 12px 0"} value={completedAt} />
    </div>)
    : null;

  if (exists) {
    if (editable) {
      return (
        <FormContainer>
          <Text bold margin={"0 0 0 5px"} value={"Edit Project"} />
          <Content>
            <TextFieldInput {...name} />
            <TextFieldInput {...description} />
            <EditContentDate>
              <FlexDiv>
                <Text bold margin={"10px 0 0 0"} value={"Creation date"} />
                <Text margin={"0 0 12px 0"} value={createdAt} />
              </FlexDiv>
              {completeContent}
            </EditContentDate>
          </Content>
          <Footer>
            <FlexDiv>
              <Button isDeleteButton onClick={onDelete}>Delete</Button>
            </FlexDiv>
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
          <ContentDate>
            <FlexDiv>
              <Text bold margin={"10px 0 0 0"} value={"Creation date"} />
              <Text margin={"0 0 12px 0"} value={createdAt} />
            </FlexDiv>
            {completeContent}
          </ContentDate>
        </ViewContent>
        <Footer style={{ marginTop: "10px" }}>
          <FlexDiv>
            <Button isDeleteButton onClick={onDelete}>Delete</Button>
          </FlexDiv>
          {!isComplete
            ? <>
                <Button primary onClick={onComplete}>Complete</Button>
                <div style={{ marginRight: "10px" }}> </div>
              </>
            : null
          }
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
          <FlexDiv />
          <Button primary onClick={onSubmit}>Create</Button>
          <div style={{ marginRight: "10px" }}> </div>
          <Button onClick={cancel}>Cancel</Button>
        </Footer>
      </FormContainer>
    );
  }
}
