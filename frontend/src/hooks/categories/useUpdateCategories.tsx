import { useMutation, useQueryClient } from "@tanstack/react-query";
import {
  updateCategory,
  type Category,
  type UpdateCategoryInput,

} from "../../services/category.services";

export function useUpdateCategory() {
  const queryClient = useQueryClient();

  return useMutation<Category, unknown, UpdateCategoryInput>({
    mutationFn: updateCategory,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["categories"] });
    },
  });
}
