import { useEffect, useState } from "react";
import { Project, Binding } from "../../types";
import { useAppDispatch } from "../../redux/hooks";
import {
  updateProject,
  createProject,
  deleteProject,
  completeProject,
} from "../../redux/actions";
import { parseDateTime } from "../../helper";

interface ProjectHookDetail {
  bindings: {
    teamId: Binding<number>,
    name: Binding<string>,
    description: Binding<string>,
  },
  createdAt: string,
  exists: boolean,
  editable: boolean,
  submit: () => void,
  delProject: () => void,
  comProject: () => void,
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
  function delProject() {
    if (project.projectId !== undefined)
      dispatch(deleteProject(project.projectId) as any);
  }
  function comProject() {
    if (project.projectId !== undefined)
      dispatch(completeProject(project.projectId) as any);
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
    createdAt: parseDateTime(project.createdAt),
    exists,
    editable,
    submit,
    delProject,
    comProject,
    setEditable,
    setProject,
  }
}
