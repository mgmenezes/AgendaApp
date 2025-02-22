import axios, { type AxiosInstance } from "axios";

export const api: AxiosInstance = axios.create({
  baseURL: "http://localhost:5162/api",
  headers: {
    "Content-Type": "application/json",
  },
});
