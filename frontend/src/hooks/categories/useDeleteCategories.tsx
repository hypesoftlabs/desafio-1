import { useMutation, useQueryClient } from "@tanstack/react-query";
import { deleteCategory } from "../../services/category.services";

export function useDeleteCategory() {
  const queryClient = useQueryClient();

  return useMutation<void, unknown, string>({
    mutationFn: deleteCategory,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["categories"] });
    },
  });
}
