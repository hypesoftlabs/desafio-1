import { api } from "./api";
import { Product } from "@/types/product";

export const getProducts = async (): Promise<Product[]> => {
  const { data } = await api.get("/product");
  return data;
};

export const createProduct = async (product: Product) => {
  await api.post("/product", product);
};