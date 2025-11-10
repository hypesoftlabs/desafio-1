import api from "../lib/api";

export interface Product {
  id: string;
  name: string;
  description: string;
  price: number;
  quantity: number;
  categoryId: string;
}

export interface ProductPage {
  data: Product[];     
  totalItems: number;
  page: number;
  pageSize: number;
}

export type CreateProductInput = {
  name: string;
  description: string;
  price: number;
  quantity: number;
  categoryId: string;
};

export type UpdateProductInput = {
  id: string;
  name: string;
  description: string;
  price: number;
  quantity: number;
  categoryId: string;
};

export type UpdateProductStockInput = {
  id: string;
  quantity: number;
};

// GET paginado
export async function getProductsPaged(
  page: number,
  pageSize: number
): Promise<ProductPage> {
  const { data } = await api.get<ProductPage>("/api/product", {
    params: { page, pageSize },
  });

  return data;
}


export async function createProduct(
  input: CreateProductInput
): Promise<Product> {
  const { data } = await api.post<Product>("/api/product", input);
  return data;
}

export async function updateProduct(
  input: UpdateProductInput
): Promise<Product> {
  const { data } = await api.put<Product>(`/api/product/${input.id}`, {
    id: input.id,
    name: input.name,
    description: input.description,
    price: input.price,
    quantity: input.quantity,
    categoryId: input.categoryId,
  });
  return data;
}

export async function deleteProduct(id: string): Promise<void> {
  await api.delete(`/api/product/${id}`);
}

export async function updateProductStock(
  input: UpdateProductStockInput
): Promise<Product> {
  const { data } = await api.patch<Product>(
    `/api/product/${input.id}/storage`,
    { ProductId: input.id,
      quantity: input.quantity }
  );
  return data;
}
