import axios from "axios";

const api = axios.create({
  baseURL: window.location.origin,
  headers: { "X-User-Token": localStorage.getItem("X-User-Token") },
  withCredentials: true,
});

api.interceptors.response.use(
  function (response) {
    if (response.status === 400) {
      return Promise.reject(response);
    }
    return response;
  },
  function (error) {
    return Promise.reject(error);
  }
);

export default api;
