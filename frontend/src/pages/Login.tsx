// src/pages/Login.tsx
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { LogIn } from "lucide-react";
import { Input } from "../components/input";
import { Button } from "../components/button";
import { useLogin } from "../hooks/auth/useLogin";
import { useAuth } from "../stores/AuthProvider";
import { extractUserFromToken } from "../stores/AuthProvider";

export const Login = () => {
  const navigate = useNavigate();
  const loginMutation = useLogin();
  const { setIsAuthenticated, setUser } = useAuth();

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    if (!username.trim() || !password.trim()) return;

    loginMutation.mutate(
      { username, password },
      {
        onSuccess: (data) => {
       
          setIsAuthenticated(true);

          const user = extractUserFromToken(data.access_token);
          if (user) {
            setUser(user);
          }

          navigate("/dashboard");
        },
      }
    );
  }

  const isLoading = loginMutation.isPending;
  const hasError = loginMutation.isError;

  return (
    <div className="flex min-h-screen items-center justify-center bg-gray-100">
      <div className="w-full max-w-md rounded-2xl bg-white p-8 shadow-lg">
        <div className="mb-6 text-center">
          <h1 className="text-2xl font-bold text-gray-800">
            Shop <span className="text-emerald-600">Manager</span>
          </h1>
          <p className="mt-1 text-sm text-gray-500">
            Faça login para acessar o painel
          </p>
        </div>

        <form onSubmit={handleSubmit} className="space-y-4">
          <div>
            <label className="mb-1 block text-sm font-medium text-gray-700">
              Usuário
            </label>
            <Input
              type="text"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              className="w-full rounded-lg border border-gray-300 bg-white py-2 px-3 text-sm text-gray-800"
              placeholder="adminuser"
            />
          </div>

          <div>
            <label className="mb-1 block text-sm font-medium text-gray-700">
              Senha
            </label>
            <Input
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              className="w-full rounded-lg border border-gray-300 bg-white py-2 px-3 text-sm text-gray-800"
              placeholder="••••••••"
            />
          </div>

          {hasError && (
            <p className="text-sm text-red-500">
              Usuário ou senha inválidos.
            </p>
          )}

          <Button
            type="submit"
            disabled={isLoading}
            className="mt-2 flex w-full items-center justify-center gap-2 rounded-lg bg-emerald-600 py-2 text-sm font-semibold text-white hover:bg-emerald-700 disabled:cursor-not-allowed disabled:opacity-70"
          >
            <LogIn className="h-4 w-4" />
            {isLoading ? "Entrando..." : "Entrar"}
          </Button>
        </form>
      </div>
    </div>
  );
};
