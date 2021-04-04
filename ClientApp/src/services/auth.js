import api from "./api";

const controller = "auth";

async function CookiesSupported() {
  const { data } = await api.get(`${controller}`);
  return data;
}

export async function UserLogin(email, password) {
  const { data } = await api.post(`${controller}/login`, { email, password });
  const cookiesAllowed = await CookiesSupported();
  localStorage.setItem("X-User-Token", data);
  return data;
}

export async function DemoLogin() {
  const { data } = await api.post(`${controller}/demologin`);
  const cookiesAllowed = await CookiesSupported();
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
