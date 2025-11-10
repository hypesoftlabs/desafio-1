// src/hooks/products/useProducts.ts
import { useQuery } from "@tanstack/react-query";
import { getProducts, type GetProductsParams } from "../../services/product.services";

export function useProducts(params: GetProductsParams) {
  return useQuery({
    queryKey: ["products", params],
    queryFn: () => getProducts(params),
  });
}
