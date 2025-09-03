import type { Category, CategoryCreate } from "@/types";
import { api } from "../lib/axios";

export async function getCategories(): Promise<Category[]> {
  const res = await api.get("/categories");
  return res.data.data;
}

export async function createCategory(category: CategoryCreate): Promise<CategoryCreate> {
  const res = await api.post("/categories", category);
  return res.data.data;
}