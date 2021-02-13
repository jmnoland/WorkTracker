import api from "./api";

const controller = "auth";

export async function UserLogin(email, password) {
  await api.post(`${controller}/login`, { email, password });
}
