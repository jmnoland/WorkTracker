import api from "./api";

const controller = "auth";

export async function UserLogin(email, password) {
  const { data } = await api.post(`${controller}/login`, { email, password });
  localStorage.setItem("X-User-Token", data);
  return data;
}

export async function DemoLogin() {
  const { data } = await api.post(`${controller}/demologin`);
  localStorage.setItem("X-User-Token", data);
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
