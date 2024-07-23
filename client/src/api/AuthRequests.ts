import axios from "axios";
import { LoginFormData } from "../types";
const URL = "http://localhost:8001";

const API = axios.create({ baseURL: URL });

export const logIn = (formData: LoginFormData) =>
  API.post("/api/auth/login", formData, {
    headers: {
      "Content-Type": "application/json",
    },
  });
