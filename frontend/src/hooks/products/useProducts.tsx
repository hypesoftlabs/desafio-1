import { useQuery } from "@tanstack/react-query";
import { getProductsPaged } from "../../services/product.services";
import type { ProductPage } from "../../services/product.services";

type UseProductsParams = {
  page: number;
  pageSize: number;
};

export function useProducts({ page, pageSize }: UseProductsParams) {
  return useQuery<ProductPage>({
    queryKey: ["products", { page, pageSize }],
    queryFn: () => getProductsPaged(page, pageSize)
  });
}
