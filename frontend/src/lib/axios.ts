import axios from "axios";

export const api = axios.create({
  baseURL: "http://localhost:5000/api", // ajuste para seu backend
  timeout: 5000,
  headers: {
    "Content-Type": "application/json",
  },
});