// src/PrivateRoute.tsx
import { useAuth } from "@/stores/AuthProvider";
import { Navigate } from "react-router-dom";


export function PrivateRoute({ children }: { children: React.ReactNode }) {
  const { isAuthenticated } = useAuth();

  if (!isAuthenticated) {
    return <Navigate to="/login" replace />;
  }

  return children;
}
