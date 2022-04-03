import React, { useState, useEffect } from "react";
import {
  ScrollableContainer,
  GenericContainer,
  Button,
} from "../../components";
import { useAppSelector, useAppDispatch } from "../../redux/hooks";
import "./project.scss";
import { getProjects } from "../../redux/actions";

const ProjectContainer = GenericContainer("project-container");
const ProjectList = GenericContainer("project-list");
const ProjectDetail = GenericContainer("project-detail");
const HeaderButton = GenericContainer("float-right");

export default function Project(): JSX.Element {
  const [loading, setLoading] = useState(false);
  const [create, setCreate] = useState(false);
  const projects = useAppSelector((state) => state.project);
  const dispatch = useAppDispatch();

  useEffect(() => {
    if (projects.length === 0) {
      dispatch(getProjects() as any)
      setLoading(true);
    } else {
      setLoading(false);
    }
    console.group("The project list");
    console.log(projects);
    console.groupEnd();
  }, [projects]);

  return (
    <ProjectContainer>
      <ProjectList>
        <ScrollableContainer
          height={100}
          contentTopMargin={10}
          header={
            <div>
              <span>Project list</span>
              <HeaderButton>
                <Button
                  primary
                  onClick={() => setCreate(true)}
                >
                  Create
                </Button>
              </HeaderButton>
            </div>
          }
        >
          {projects.map(project => {
            console.log(project);
            return (<div key={project.projectId}>
              <div>{project.name}</div>
            </div>);
          })}
        </ScrollableContainer>
      </ProjectList>
      <ProjectDetail>

      </ProjectDetail>
    </ProjectContainer>
  );
}
