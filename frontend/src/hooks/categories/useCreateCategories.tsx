import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createCategory, type Category, type CreateCategoryInput } from "../../services/category.services";


export function useCreateCategory() {
  const queryClient = useQueryClient();

  return useMutation<Category, unknown, CreateCategoryInput>({
    mutationFn: createCategory,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["categories"] });
    },
  });
}
