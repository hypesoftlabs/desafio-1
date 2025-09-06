import type { Category, CategoryCreate, CategoryUpdate } from "@/types";
import { api } from "../lib/axios";

export async function getCategories(): Promise<Category[]> {
  const res = await api.get("/categories");
  return res.data.data;
}

export async function createCategory(category: CategoryCreate): Promise<CategoryCreate> {
  const res = await api.post("/categories", category);
  return res.data.data;
}

export async function updateCategory(category: CategoryUpdate): Promise<CategoryUpdate> {
  const res = await api.put(`/categories/${category.id}`, category);
  return res.data.data;
};

export const deleteCategory = async (id: string): Promise<void> => {
  await api.delete(`/categories/${id}`);
};
