import api from "../lib/api";
import type { Product } from "./product.services";

export interface DashboardSummary {
  totalProducts: number;
  storageValueTotal: number;
  lowStorageProducts: Product[];
}

export interface DashboardCategoryGraphItem {
  categoryName: string;
  productQuantity: number;
}

export async function getDashboardSummary(): Promise<DashboardSummary> {
  const { data } = await api.get<DashboardSummary>("/api/Dashboard/summary");
  return data;
}

export async function getDashboardGraph(): Promise<DashboardCategoryGraphItem[]> {
  const { data } = await api.get<DashboardCategoryGraphItem[]>(
    "/api/Dashboard/graphByCategory"
  );
  return data;
}
