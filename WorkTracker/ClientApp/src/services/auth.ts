import api from "./api";

const controller = "auth";

export async function UserLogin(email: string, password: string): Promise<string> {
  const { data } = await api.post(`${controller}/login`, { email, password });
  localStorage.setItem("X-User-Token", data);
  return data;
}

export async function DemoLogin(): Promise<string> {
  const { data } = await api.post(`${controller}/demologin`);
  localStorage.setItem("X-User-Token", data);
  return data;
}

export async function UserRegister(email: string, name: string, password: string): Promise<string> {
  const { data } = await api.post(`${controller}/register`, {
    email,
    password,
    name,
  });
  return data;
}
