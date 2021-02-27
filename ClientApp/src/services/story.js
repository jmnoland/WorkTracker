import api from "./api";

const controller = "story";

export async function GetStories(stateId) {
  const { data } = await api.get(`${controller}/${stateId}`);
  return data;
}

export async function CreateStory(title, description, state, tasks) {
  const { data } = await api.post(`${controller}`, {
    title,
    description,
    StateId: state,
    tasks,
  });
  return data;
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
