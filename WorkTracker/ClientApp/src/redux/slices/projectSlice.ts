import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { Project } from "../../types";
import { CompleteProject, CreateProject, DeleteProject, GetProjects, UpdateProject } from "../../services/project";

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
      projectList.forEach(project => {
        state.push(project);
      });
    });
    builder.addCase(createProject.fulfilled, (state, action) => {
      const { newProject } = action.payload;
      state.push(newProject);
    });
    builder.addCase(updateProject.fulfilled, (state, action) => {
      const { updatedProject } = action.payload;
      state.forEach(project => {
        if (project.projectId === updatedProject.projectId) {
          project.teamId = updatedProject.teamId;
          project.name = updatedProject.name;
          project.description = updatedProject.description;
        }
      });
    });
    builder.addCase(completeProject.fulfilled, (state, action) => {
      const projectId = action.payload;
      state.forEach(project => {
        if (project.projectId === projectId)
          project.completedAt = new Date().toString();
      });
    });
    builder.addCase(deleteProject.fulfilled, (state, action) => {
      const projectId = action.payload;
      let delIndex = 0;
      state.forEach((project, index) => {
        if (project.projectId === projectId) delIndex = index;
      });
      state.splice(delIndex, 1);
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
