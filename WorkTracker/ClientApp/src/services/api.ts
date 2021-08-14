import axios from "axios";

const api = axios.create({
  baseURL: window.location.origin,
  transformRequest: [
    function (data, headers) {
      headers.Authorization = `Bearer ${localStorage.getItem("X-User-Token")}`;
      headers["Content-Type"] = "application/json; charset=utf-8";
      headers.Accept = "application/json";
      return JSON.stringify(data);
    },
  ],
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
