// src/hooks/products/useUpdateProductStock.ts
import { useMutation, useQueryClient } from "@tanstack/react-query";
import {
  updateProductStock,
} from "../../services/product.services";
import type {
  UpdateProductStockInput,
  Product,
} from "../../services/product.services";

export function useUpdateProductStock() {
  const queryClient = useQueryClient();

  return useMutation<Product, unknown, UpdateProductStockInput>({
    mutationFn: updateProductStock,
    onSuccess: (_, variables) => {
      queryClient.invalidateQueries({ queryKey: ["products"] });
      if (variables.id) {
        queryClient.invalidateQueries({ queryKey: ["products", variables.id] });
      }
    },
  });
}
