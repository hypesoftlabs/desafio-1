import type { Category } from "@/types";
import { api } from "../lib/axios";

export async function getCategories(): Promise<Category[]> {
  const { data } = await api.get("/categories");
  return data;
}