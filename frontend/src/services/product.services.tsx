import api from "../lib/api";

export interface Product {
  id: string;
  name: string;
  description: string;
  price: number;
  quantity: number;
  categoryId: string;
}

export type CreateProductInput = Omit<Product, "id">;

export async function getProducts() {
  const { data } = await api.get<Product[]>("/api/product");
  return data;
}

export async function createProduct(input: CreateProductInput) {
  const { data } = await api.post<Product>("/api/product", input);
  return data;
}
