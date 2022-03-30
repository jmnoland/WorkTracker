import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { Project } from "../../types";
import {
  GetProjects,
  DeleteProject,
  CreateProject,
  UpdateProject,
  CompleteProject,
} from "../../services/project";

function getInitialState() {
  const initialState: Project[] = [];
  return initialState;
}

const getProjects = createAsyncThunk(
  "project/get",
  async () => {
    const projectList = await GetProjects();
    return { projectList };
  }
);
const createProject = createAsyncThunk(
  "project/create",
  async (project: Project) => {
    const newProject = await CreateProject(project);
    return { newProject };
  }
);
const updateProject = createAsyncThunk(
  "project/update",
  async (project: Project) => {
    const updatedProject = await UpdateProject(project);
    return { updatedProject };
  }
);
const deleteProject = createAsyncThunk(
  "project/delete",
  async (projectId: number) => {
    await DeleteProject(projectId);
    return projectId;
  }
);
const completeProject = createAsyncThunk(
  "project/complete",
  async (projectId: number) => {
    await CompleteProject(projectId);
    return projectId;
  }
);

export const projectSlice = createSlice({
  name: "project",
  initialState: getInitialState(),
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(getProjects.fulfilled, (state, action) => {
      const { projectList } = action.payload;
      state = projectList;
    });
    builder.addCase(createProject.fulfilled, (state, action) => {
      const { newProject } = action.payload;
      state.push(newProject);
    });
    builder.addCase(updateProject.fulfilled, (state, action) => {
      const { updatedProject } = action.payload;
      const temp = state.filter(w => w.ProjectId !== updatedProject.ProjectId);
      temp.push(updatedProject);
      state = temp;
    });
    builder.addCase(completeProject.fulfilled, (state, action) => {
      const projectId = action.payload;
      state.forEach(project => {
        if (project.ProjectId === projectId) project.CompletedAt = new Date();
      });
    });
    builder.addCase(deleteProject.fulfilled, (state, action) => {
      const projectId = action.payload;
      state = state.filter(project => project.ProjectId !== projectId);
    });
  },
});

export {
  getProjects,
  createProject,
  updateProject,
  completeProject,
  deleteProject,
};
export default projectSlice.reducer;
