// src/stores/AuthProvider.tsx
import React, { createContext, useContext, useEffect, useState, type ReactNode } from "react";
import api from "@/lib/api";

interface User {
  name: string;
  username: string;
  role: string;
}

interface AuthContextValue {
  isAuthenticated: boolean;
  user: User | null;
  setIsAuthenticated: (value: boolean) => void;
  setUser: (user: User | null) => void;
  logout: () => void;
}

export const AuthContext = createContext<AuthContextValue>({
  isAuthenticated: false,
  user: null,
  setIsAuthenticated: () => {},
  setUser: () => {},
  logout: () => {},
});

function parseJwt(token: string): any | null {
  try {
    const base64 = token.split(".")[1].replace(/-/g, "+").replace(/_/g, "/");
    const jsonPayload = atob(base64);
    return JSON.parse(jsonPayload);
  } catch {
    return null;
  }
}

export function extractUserFromToken(token: string): User | null {
  const payload = parseJwt(token);
  if (!payload) return null;

  const name: string =
    payload.name ||
    (payload.given_name && payload.family_name
      ? `${payload.given_name} ${payload.family_name}`
      : payload.preferred_username ||
        payload.username ||
        "Usuário");

  const username: string =
    payload.preferred_username || payload.username || name;

  let role = "Usuário";
  if (payload.realm_access?.roles?.length) {
    if (payload.realm_access.roles.includes("admin")) {
      role = "Admin";
    } else {
      role = payload.realm_access.roles[0];
    }
  }

  return { name, username, role };
}

export function AuthProvider({ children }: { children: ReactNode }) {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [user, setUser] = useState<User | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const token = localStorage.getItem("access_token");

    if (token) {
      setIsAuthenticated(true);
      api.defaults.headers.common.Authorization = `Bearer ${token}`;
      const extracted = extractUserFromToken(token);
      if (extracted) {
        setUser(extracted);
      }
    }

    setLoading(false);
  }, []);

  function logout() {
    localStorage.removeItem("access_token");
    delete api.defaults.headers.common.Authorization;
    setIsAuthenticated(false);
    setUser(null);
  }

  if (loading) {
    return <div>Carregando autenticação...</div>;
  }

  return (
    <AuthContext.Provider
      value={{ isAuthenticated, user, setIsAuthenticated, setUser, logout }}
    >
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  return useContext(AuthContext);
}
