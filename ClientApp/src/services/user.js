import api from "./api";

const controller = "user";

export async function CreateUser(email, password) {
  const name = email.split("@")[0];
  await api.post(`${controller}/register`, {
    email,
    password,
    name,
  });
}
