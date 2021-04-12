import api from "./api";

const controller = "story";

export async function GetStories(stateId) {
  const { data } = await api.get(`${controller}/${stateId}`);
  return data;
}

// includes archived stories
export async function GetAllStories(stateId) {
  const { data } = await api.get(`${controller}/archive/${stateId}`);
  return data;
}

export async function CreateStory(
  title,
  description,
  storyPosition,
  state,
  tasks
) {
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
  storyId,
  listOrder,
  title,
  description,
  state,
  tasks
) {
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

export async function DeleteStory(storyId) {
  await api.delete(`${controller}/${storyId}`);
}

export async function GetStoryTasks(storyId) {
  const { data } = await api.get(`${controller}/task/${storyId}`);
  return data;
}

export async function DeleteTask(taskId) {
  await api.delete(`${controller}/task/${taskId}`);
}

export async function ChangeState(storyId, payload) {
  const { data } = await api.patch(
    `${controller}/update/state/${storyId}`,
    payload
  );
  return data;
}

export async function OrderUpdate(payload) {
  const { data } = await api.patch(`${controller}/update/order`, payload);
  return data;
}
