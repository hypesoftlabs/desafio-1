// src/hooks/products/useCreateProduct.ts
import { useMutation, useQueryClient } from "@tanstack/react-query";
import {
  createProduct,
} from "../../services/product.services";
import type {
  CreateProductInput,
  Product,
} from "../../services/product.services";

export function useCreateProduct() {
  const queryClient = useQueryClient();

  return useMutation<Product, unknown, CreateProductInput>({
    mutationFn: createProduct,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["products"] });
    },
  });
}
