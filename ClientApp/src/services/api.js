import axios from "axios";

export default axios.create({
  baseURL: window.location.origin,
  headers: { "X-User-Token": localStorage.getItem("X-User-Token") },
  withCredentials: true,
});
