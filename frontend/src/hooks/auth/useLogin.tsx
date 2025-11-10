// src/hooks/auth/useLogin.ts
import { login, LoginRequest, LoginResponse } from "@/services/auth.services";
import { useMutation } from "@tanstack/react-query";


export function useLogin() {
  return useMutation<LoginResponse, unknown, LoginRequest>({
    mutationFn: login,
  });
}
