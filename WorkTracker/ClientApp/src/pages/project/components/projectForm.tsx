import React, { useEffect } from "react";
import { useProject } from "../hooks";
import { Project } from "../../../types";
import {
  Button,
  GenericContainer,
  Text,
  TextFieldInput,
} from "../../../components";

const Content = GenericContainer();

interface ProjectFormProps {
  initialValues: Project;
  onCancel: () => void;
}
export function ProjectForm({ initialValues, onCancel }
                              : ProjectFormProps): JSX.Element {
  const {
    bindings: { name, description },
    exists,
    editable,
    submit,
    setEditable,
    setProject,
  } = useProject(initialValues);

  useEffect(() => {
    setProject(initialValues);
  }, [initialValues]);

  if (exists) {
    if (editable) {
      return (
        <Content>
          <TextFieldInput {...name} />
          <TextFieldInput {...description} />
          <Button primary onClick={submit}>Save</Button>
          <Button onClick={onCancel}>Cancel</Button>
        </Content>
      );
    }
    return (
      <Content>
        <Text value={name.value} />
        <Text value={description.value} />
        <Button primary onClick={() => setEditable(true)}>Edit</Button>
        <Button onClick={onCancel}>Cancel</Button>
      </Content>
    );
  } else {
    return (
      <Content>
        <Text value={"Create new Project"} />
        <TextFieldInput {...name} />
        <TextFieldInput {...description} />
        <Button onClick={submit}>Create</Button>
        <Button onClick={onCancel}>Cancel</Button>
      </Content>
    );
  }
}
