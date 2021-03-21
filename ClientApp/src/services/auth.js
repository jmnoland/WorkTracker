import api from "./api";

const controller = "auth";

export async function UserLogin(email, password) {
  const { data } = await api.post(`${controller}/login`, { email, password });
  return data;
}

export async function UserRegister(email, name, password) {
  const { data } = await api.post(`${controller}/register`, {
    email,
    password,
    name,
  });
  return data;
}
