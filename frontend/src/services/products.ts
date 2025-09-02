import axios from "axios";
import { Category, Product } from "@/types";

const api = axios.create({
  baseURL: "http://localhost:5000/api",
});

// GET /api/products
export const getProducts = () =>
  api.get<{ message: string; data: Product[] }>("/products");

// POST /api/products
export const createProduct = (data: Partial<Product>) =>
  api.post<{ message: string; data: Product }>("/products", data);

// PUT /api/products
export const updateProduct = (id: number, data: Partial<Product>) =>
  api.put<{ message: string; data: Product }>(`/products/${id}`, data);

// DELETE /api/products
export const deleteProduct = (id: number) =>
  api.delete<{ message: string; data: Product }>(`/products/${id}`);

// GET /api/categories
export const getCategories = () =>
  api.get<{ message: string; data: Category[] }>("/categories");

// POST /api/categories
export const createCategory = (data: Partial<Category>) =>
  api.post<{ message: string; data: Category }>("/categories", data);

// PUT /api/categories/{id}
export const updateCategory = (id: number, data: Partial<Category>) =>
  api.put<{ message: string; data: Category }>(`/categories/${id}`, data);

// DELETE /api/categories/{id}
export const deleteCategory = (id: number) =>
  api.delete<{ message: string; data: Category }>(`/categories/${id}`);