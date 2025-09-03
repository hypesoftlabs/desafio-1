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

import { useCreateCategory } from "../../hooks/useCategories";

// Zod schema para validação
const categorySchema = z.object({
  name: z
    .string()
    .min(2, {
      message:
        "Name must be at least 2 characters.",
    }),
});

type CategoryFormValues = z.infer<
  typeof categorySchema
>;

export function CreateCategoryForm() {
  const { mutate } =
    useCreateCategory();

  const form = useForm<CategoryFormValues>({
    resolver: zodResolver(categorySchema),
    defaultValues: {
      name: "",
    },
  });

  function onSubmit(values: CategoryFormValues) {
    mutate(values);
    alert("Category created successfully!");
  }

  return (
    <Form {...form}>
      <form
        onSubmit={form.handleSubmit(onSubmit)}
        className="space-y-4"
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
            ? "Creating..."
            : "Create Category"}
        </Button>
      </form>
    </Form>
  );
}

export default CreateCategoryForm;
