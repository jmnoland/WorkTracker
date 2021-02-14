import api from "./api";

const controller = "auth";

export async function UserLogin(email, password) {
  const { data } = await api.post(`${controller}/login`, { email, password });
  return data;
}
