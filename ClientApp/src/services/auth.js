import api from "./api";

const controller = "auth";

export async function UserLogin(email, password) {
  await api.post(`${controller}/login`, { email, password });
}

export async function RegisterUser(email, password, confirmPassword) {
  await api.post(`${controller}/register`, {
    email,
    password,
    confirmPassword,
  });
}
