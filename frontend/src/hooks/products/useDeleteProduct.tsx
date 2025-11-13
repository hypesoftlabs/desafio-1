// src/hooks/products/useDeleteProduct.ts
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { deleteProduct } from "../../services/product.services";

export function useDeleteProduct() {
  const queryClient = useQueryClient();

  return useMutation<void, unknown, string>({
    mutationFn: deleteProduct,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["products"] });
    },
  });
}
