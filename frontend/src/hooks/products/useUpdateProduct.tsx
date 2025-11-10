// src/hooks/products/useUpdateProduct.ts
import { useMutation, useQueryClient } from "@tanstack/react-query";
import {
  updateProduct,
} from "../../services/product.services";
import type {
  UpdateProductInput,
  Product,
} from "../../services/product.services";

export function useUpdateProduct() {
  const queryClient = useQueryClient();

  return useMutation<Product, unknown, UpdateProductInput>({
    mutationFn: updateProduct,
    onSuccess: (_, variables) => {
      // atualiza lista e, se existir, o cache do produto individual
      queryClient.invalidateQueries({ queryKey: ["products"] });
      if (variables.id) {
        queryClient.invalidateQueries({ queryKey: ["products", variables.id] });
      }
    },
  });
}
