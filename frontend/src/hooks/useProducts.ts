import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { getProducts } from "../services/products";
import { createProduct } from "@/services/products";
import type { Product } from "@/types";

export function useProducts() {
  return useQuery({
    queryKey: ["products"],
    queryFn: getProducts,
  });
}

export function useCreateProduct() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (product: Product) => createProduct(product),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["products"] });
    },
  });
}