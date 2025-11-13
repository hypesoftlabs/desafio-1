// src/services/auth.services.ts
import axios from "axios";
import api from "../lib/api";

const KEYCLOAK_TOKEN_URL =
  "/auth/realms/shop-realm/protocol/openid-connect/token"; // <-- AQUI

const CLIENT_ID = "shop-api";

export interface LoginRequest {
  username: string;
  password: string;
}

export interface LoginResponse {
  access_token: string;
  refresh_token: string;
  expires_in: number;
  refresh_expires_in: number;
  token_type: string;
  scope: string;
}

export async function login({
  username,
  password,
}: LoginRequest): Promise<LoginResponse> {
  const body = new URLSearchParams({
    grant_type: "password",
    client_id: CLIENT_ID,
    username,
    password,
  });

  const { data } = await axios.post<LoginResponse>(KEYCLOAK_TOKEN_URL, body, {
    headers: {
      "Content-Type": "application/x-www-form-urlencoded",
    },
  });

  localStorage.setItem("access_token", data.access_token);
  api.defaults.headers.common.Authorization = `Bearer ${data.access_token}`;

  return data;
}
