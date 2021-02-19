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

export async function GetDetails(userId) {
  const { data } = await api.get(`${controller}/details/${userId}`);
  return data;
}