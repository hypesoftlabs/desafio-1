import { api } from "./api";
import { Category } from "@/types/category";

export const getCategories = async (): Promise<Category[]> => {
  const { data } = await api.get("/category");
  return data;
};

export const createCategory = async (data: { name: string }) => {
  try {
    const response = await api.post("/category", { category: data });
    return response.data;
  } catch (error: any) {
    console.error("Erro ao criar categoria:", error.response?.data || error);
    throw error;
  }
};