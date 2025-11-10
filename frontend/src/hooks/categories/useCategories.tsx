import { useQuery } from "@tanstack/react-query";
import { getCategories, type Category } from "../../services/category.services";


export function useCategories() {
  return useQuery<Category[]>({
    queryKey: ["categories"],
    queryFn: getCategories,
  });
}
