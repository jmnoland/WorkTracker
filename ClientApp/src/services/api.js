import axios from "axios";

export default axios.create({
  baseURL: window.location.origin,
  "X-User-Token": "test",
});
