import { useEffect, useState } from "react";
import { Project, Binding } from "../../types";
import { useAppDispatch } from "../../redux/hooks";
import { updateProject, createProject } from "../../redux/actions";

interface ProjectHookDetail {
  bindings: {
    teamId: Binding<number>,
    name: Binding<string>,
    description: Binding<string>,
  },
  exists: boolean,
  editable: boolean,
  submit: () => void,
  setEditable: (val: boolean) => void;
  setProject: (project: Project) => void,
}

function getProjectWithDefaults(project: Project) {
  return {
    teamId: project.teamId ?? -1,
    projectId: project.projectId,
    name: project.name ?? "",
    description: project.description ?? "",
    createdAt: project.createdAt,
    completedAt: project.completedAt,
  };
}

export function useProject(initialValues: Project): ProjectHookDetail {
  const [project, _setProject] = useState(getProjectWithDefaults(initialValues));
  const [exists, setExists] = useState(project.projectId !== undefined);
  const [editable, setEditable] = useState(!exists);
  const dispatch = useAppDispatch();

  function setProject(project: Project) {
    setEditable(false);
    _setProject(getProjectWithDefaults(project));
  }

  useEffect(() => {
    setExists(project.projectId !== undefined);
  }, [project]);
  useEffect(() => {
    setEditable(!exists);
  }, [exists]);

  function submit() {
    if (exists) {
      dispatch(updateProject(project) as any);
    } else {
      dispatch(createProject(project) as any);
    }
  }

  return {
    bindings: {
      teamId: {
        value: project.teamId,
        onChange: (val: number) => setProject({ ...project, teamId: val }),
      },
      name: {
        value: project.name,
        onChange: (val: string) => setProject({ ...project, name: val }),
      },
      description: {
        value: project.description,
        onChange: (val: string) => setProject({ ...project, description: val }),
      },
    },
    exists,
    editable,
    submit,
    setEditable,
    setProject,
  }
}
