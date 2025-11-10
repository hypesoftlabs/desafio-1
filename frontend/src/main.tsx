// src/main.tsx ou index.tsx
import "./index.css";
import React from "react";
import ReactDOM from "react-dom/client";
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import { Layout } from "./components/layout";
import { Products } from "./pages/Products";
import { Categories } from "./pages/Categories";
import { Stock } from "./pages/Stock";
import { Dashboard } from "./pages/Dashboard";
import { Login } from "./pages/Login";
import {
  QueryClient,
  QueryClientProvider,
} from "@tanstack/react-query";
import { AuthProvider } from "./stores/AuthProvider";
import { PrivateRoute } from "./components/auth/PrivateRoute";


const queryClient = new QueryClient();

const router = createBrowserRouter([
  {
    path: "/login",
    element: <Login />,
  },
  {
    path: "/",
    element: (
      <PrivateRoute>
        <Layout />
      </PrivateRoute>
    ),
    children: [
      {
        path: "/dashboard",
        element: <Dashboard />,
      },
      {
        path: "/produtos",
        element: <Products />,
      },
      {
        path: "/categorias",
        element: <Categories />,
      },
      {
        path: "/estoque",
        element: <Stock />,
      },
    ],
  },
]);

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <QueryClientProvider client={queryClient}>
      <AuthProvider>
        <RouterProvider router={router} />
      </AuthProvider>
    </QueryClientProvider>
  </React.StrictMode>
);
