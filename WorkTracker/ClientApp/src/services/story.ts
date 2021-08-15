import { Dictionary } from "../types";
import { Story } from "../types/story";
import { Task } from "../types/task";
import api from "./api";

const controller = "story";

export async function GetStories(stateId: number): Promise<Story[]> {
  const { data } = await api.get(`${controller}/${stateId}`);
  return data;
}

// includes archived stories
export async function GetAllStories(stateId: number): Promise<Story[]> {
  const { data } = await api.get(`${controller}/archive/${stateId}`);
  return data;
}

export async function CreateStory(
  title: string,
  description: string,
  storyPosition: number,
  state: number,
  tasks: Task[]
): Promise<null> {
  const { data } = await api.post(`${controller}`, {
    title,
    tasks,
    description,
    StateId: state,
    listOrder: storyPosition,
  });
  return data;
}

export async function UpdateStory(
  storyId: number,
  listOrder: number,
  title: string,
  description: string,
  state: number,
  tasks: Task[]
): Promise<null> {
  tasks &&
    tasks.forEach((val) => {
      if (val.new) {
        val.taskId = 0;
      }
    });
  const { data } = await api.patch(`${controller}`, {
    storyId,
    stateId: state,
    title,
    description,
    listOrder,
    tasks,
  });
  return data;
}

export async function DeleteStory(storyId: number): Promise<void> {
  await api.delete(`${controller}/${storyId}`);
}

export async function GetStoryTasks(storyId: number): Promise<Task[]> {
  const { data } = await api.get(`${controller}/task/${storyId}`);
  return data;
}

export async function DeleteTask(taskId: number): Promise<void> {
  await api.delete(`${controller}/task/${taskId}`);
}

export async function ChangeState(storyId: number, payload: { stateId: number, stories: Dictionary<number> }): Promise<void> {
  const { data } = await api.patch(
    `${controller}/update/state/${storyId}`,
    payload
  );
  return data;
}

export async function OrderUpdate(payload: { stateId: number, stories: Dictionary<number> }): Promise<void> {
  const { data } = await api.patch(`${controller}/update/order`, payload);
  return data;
}
