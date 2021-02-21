import api from "./api";

const controller = "story";

export async function CreateStory(title, description, state, tasks) {
  const { data } = await api.post(`${controller}`, {
    title,
    description,
    StateId: state,
    tasks,
  });
  return data;
}
