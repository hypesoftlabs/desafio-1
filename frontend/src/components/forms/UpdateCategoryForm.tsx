import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { z } from "zod";

import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";

import { useUpdateCategory } from "../../hooks/useCategories";
import type { Category } from "@/types";

// Zod schema para validação
const categorySchema = z.object({
  name: z.string().min(2, {
    message:
      "Name must be at least 2 characters.",
  }),
});

type CategoryFormValues = z.infer<
  typeof categorySchema
>;

interface UpdateCategoryFormProps {
  category: Category;
  onSuccess?: () => void;
}

export function UpdateCategoryForm({
  category,
  onSuccess,
}: UpdateCategoryFormProps) {
  const { mutate } = useUpdateCategory();

  const form = useForm<CategoryFormValues>({
    resolver: zodResolver(categorySchema),
    defaultValues: {
      name: category.name || "",
    },
  });

  function onSubmit(values: CategoryFormValues) {
    mutate(
      { id: category.id, name: values.name },
      {
        onSuccess: () => {
          alert("Category updated successfully!");
          if (onSuccess) onSuccess();
        },
      }
    );
  }

  return (
    <Form {...form}>
      <form
        onSubmit={form.handleSubmit(onSubmit)}
        className="space-y-4 w-full max-w-md"
      >
        {/* Name */}
        <FormField
          control={form.control}
          name="name"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Name</FormLabel>
              <FormControl>
                <Input
                  {...field}
                  placeholder="Category name"
                  className="input bg-zinc-300"
                  required
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />

        <Button
          type="submit"
          disabled={form.formState.isSubmitting}
        >
          {form.formState.isSubmitting
            ? "Updating..."
            : "Update Category"}
        </Button>
      </form>
    </Form>
  );
}

export default UpdateCategoryForm;
