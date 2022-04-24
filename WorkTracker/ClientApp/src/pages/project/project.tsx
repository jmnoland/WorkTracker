import React, { useState, useEffect } from "react";
import {
  ScrollableContainer,
  GenericContainer,
  Button,
  Loading,
} from "../../components";
import { getProjects } from "../../redux/actions";
import { Project } from "../../types";
import { useAppSelector, useAppDispatch } from "../../redux/hooks";
import "./project.scss";
import { ProjectForm } from "./components/projectForm";

const ProjectPageContainer = GenericContainer("project-page");
const ProjectList = GenericContainer("project-list");
const ProjectDetail = GenericContainer("project-detail");
const HeaderButton = GenericContainer("float-right");
const ProjectContainer = GenericContainer("project-container");

export default function ProjectPage(): JSX.Element {
  const [loading, setLoading] = useState(false);
  const [
    selectedProject, setSelectedProject,
  ] = useState<Project | null>(null);
  const projects = useAppSelector((state) => state.project);
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(getProjects() as any)
    setLoading(true);
  },[]);
  useEffect(() => {
    setLoading(false);
  }, [projects]);

  function cancel() {
    setSelectedProject(null);
  }

  if (loading) {
    return (
      <ProjectPageContainer>
        <Loading small={false} primary />
      </ProjectPageContainer>);
  }

  return (
    <ProjectPageContainer>
      <ProjectList>
        <ScrollableContainer
          height={100}
          usePercent
          contentTopMargin={10}
          header={
            <div>
              <span>Project list</span>
              <HeaderButton>
                <Button
                  primary
                  onClick={() => setSelectedProject({})}
                >
                  Create
                </Button>
              </HeaderButton>
            </div>
          }
        >
          {projects.map(project => {
            return (
              <ProjectContainer
                key={project.projectId}
                onClick={() => setSelectedProject(project)}
              >
                <div>{project.name}</div>
              </ProjectContainer>
            );
          })}
        </ScrollableContainer>
      </ProjectList>
      <ProjectDetail>
        {selectedProject !== null
          ? <ProjectForm
              initialValues={selectedProject}
              onCancel={cancel}
            />
          : "Select or create a project"
        }
      </ProjectDetail>
    </ProjectPageContainer>
  );
}
