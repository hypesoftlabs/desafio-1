import type { Category } from "@/types";
import { api } from "../lib/axios";

export async function getCategories(): Promise<Category[]> {
  const res = await api.get("/categories");
  return res.data.data;
}