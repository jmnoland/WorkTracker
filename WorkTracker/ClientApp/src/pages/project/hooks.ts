import { useState } from "react";
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
  submit: () => void,
  setProject: (project: Project) => void,
}

export function useProject(initialValues: Project): ProjectHookDetail {
  const [project, setProject] = useState(initialValues);
  const exists = project.projectId !== undefined;
  const dispatch = useAppDispatch();

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
    submit,
    setProject,
  }
}
