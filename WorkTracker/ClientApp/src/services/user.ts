import { UserDetail } from "../types";
import api from "./api";

const controller = "user";

export async function CreateUser(email: string, password: string): Promise<void> {
  const name = email.split("@")[0];
  await api.post(`${controller}/register`, {
    email,
    password,
    name,
  });
}

export async function GetDetails(): Promise<UserDetail> {
  const { data } = await api.get(`${controller}/details`);
  return data;
}
