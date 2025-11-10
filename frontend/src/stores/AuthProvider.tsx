// src/AuthProvider.tsx
import React, { useEffect, useState, type ReactNode } from "react";
import keycloak from "../lib/keycloak";


interface AuthContextValue {
  isAuthenticated: boolean;
}

export const AuthContext = React.createContext<AuthContextValue>({
  isAuthenticated: false,
});

export function AuthProvider({ children }: { children: ReactNode }) {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    keycloak
      .init({
        onLoad: "login-required",
        checkLoginIframe: false,
        pkceMethod: "S256",
      })
      .then((authenticated) => {
        setIsAuthenticated(authenticated);

        if (authenticated) {
          localStorage.setItem("access_token", keycloak.token || "");
        }

        setLoading(false);
      })
      .catch(() => {
        setIsAuthenticated(false);
        setLoading(false);
      });
  }, []);

  if (loading) {
    return <div>Carregando autenticação...</div>;
  }

  return (
    <AuthContext.Provider value={{ isAuthenticated }}>
      {children}
    </AuthContext.Provider>
  );
}
