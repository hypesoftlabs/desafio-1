import { api } from "./api";
import { Category } from "@/types/category";

export const getCategories = async (): Promise<Category[]> => {
  const { data } = await api.get("/category");
  return data;
};

export const createCategory = async (category: Category) => {
  await api.post("/category", category);
};