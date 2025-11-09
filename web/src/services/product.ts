import { api } from "./api";
import { Product } from "@/types/product";

export const getProducts = async (): Promise<Product[]> => {
  const { data } = await api.get("/product");
  return data;
};

export const createProduct = async (product: Product) => {
  console.log("Enviando produto:", product);
  await api.post("/product", product);
};

export async function updateProductStock(product: Product, newStock: number): Promise<Product> {
  const response = await api.put(`/Product/${product.id}`, {
    ...product,
    stockQuantity: newStock,
  });
  return response.data;
}

export async function deleteProduct(id: string): Promise<void> {
  await api.delete(`/Product/${id}`);
}