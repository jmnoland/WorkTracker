import { Project } from "../types";
import api from "./api";

const controller = "project";

export async function GetProjects(): Promise<Project[]> {
  const { data } = await api.get(`${controller}`);
  return data;
}

export async function UpdateProject(project: Project): Promise<Project> {
  const { data } = await api.patch(`${controller}`, project);
  return data;
}

export async function CreateProject(project: Project): Promise<Project> {
  const { data } = await api.post(`${controller}`, project);
  return data;
}

export async function DeleteProject(projectId: number): Promise<void> {
  await api.post(`${controller}/delete/${projectId}`);
}

export async function CompleteProject(projectId: number): Promise<void> {
  await api.post(`${controller}/complete/${projectId}`);
}
