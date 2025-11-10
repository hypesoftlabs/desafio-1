import { useQuery } from "@tanstack/react-query";
import { getProducts } from "../services/product.services";

export function useProducts() {
  return useQuery({
    queryKey: ["products"],
    queryFn: getProducts,
  });
}
