import type { LowStockResponse, ProductsByCategoryResponse, TotalProductsResponse, TotalStockValueResponse } from "@/types";
import { api } from "../lib/axios";

export async function getTotalProducts(): Promise<number> {
  const res = await api.get<TotalProductsResponse>("/dashboard/total-products");
  return res.data.totalProducts;
}

export async function getProductsByCategory(): Promise<Record<string, number>> {
  const res = await api.get<ProductsByCategoryResponse>("/dashboard/products-by-category");
  return res.data.productsByCategory;
}

export async function getTotalStockValue(): Promise<number> {
  const res = await api.get<TotalStockValueResponse>("/dashboard/total-stock-value");
  return res.data.totalStockValue;
}

export async function getLowStock(): Promise<number> {
  const res = await api.get<LowStockResponse>("/dashboard/low-stock");
  return res.data.lowStockProducts;
}

