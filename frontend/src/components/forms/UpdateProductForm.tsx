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
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";

import { useCategories } from "@/hooks/useCategories";
import { useUpdateProduct } from "@/hooks/useProducts";
import type { Product } from "@/types";
import { Input } from "../ui/input";

// Zod schema
const productSchema = z.object({
  name: z.string().min(2),
  description: z.string().min(2),
  price: z
    .string()
    .refine(
      (val) =>
        !isNaN(Number(val)) && Number(val) > 0
    ),
  categoryId: z.string().min(1),
  stock: z
    .string()
    .refine(
      (val) =>
        Number.isInteger(Number(val)) &&
        Number(val) >= 0
    ),
});

type ProductFormValues = z.infer<
  typeof productSchema
>;

interface EditProductFormProps {
  product: Product;
  onSuccess?: () => void;
}

export function UpdateProductForm({
  product,
  onSuccess,
}: EditProductFormProps) {
  const { mutate } = useUpdateProduct();
  const { data: categories = [] } =
    useCategories();

  const form = useForm<ProductFormValues>({
    resolver: zodResolver(productSchema),
    defaultValues: {
      name: product.name,
      description: product.description,
      price: product.price.toString(),
      categoryId: product.categoryId.toString(),
      stock: product.stock.toString(),
    },
  });

  function onSubmit(values: ProductFormValues) {
    const payload = {
      ...values,
      price: parseFloat(values.price),
      categoryId: parseInt(values.categoryId, 10),
      stock: parseInt(values.stock, 10),
    };

    mutate(
      {
        id: product.id ?? 0,
        ...payload,
      },
      {
        onSuccess: () => {
          alert("Product updated successfully!");
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
                  placeholder="Product name"
                  className="input bg-zinc-300"
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />

        {/* Description */}
        <FormField
          control={form.control}
          name="description"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Description</FormLabel>
              <FormControl>
                <Input
                  {...field}
                  placeholder="Product description"
                  className="input bg-zinc-300"
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />

        {/* Price */}
        <FormField
          control={form.control}
          name="price"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Price</FormLabel>
              <FormControl>
                <Input
                  {...field}
                  placeholder="10.5"
                  className="input bg-zinc-300"
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />

        {/* Stock */}
        <FormField
          control={form.control}
          name="stock"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Stock</FormLabel>
              <FormControl>
                <Input
                  {...field}
                  placeholder="15"
                  className="input bg-zinc-300"
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />

        {/* Category */}
        <FormField
          control={form.control}
          name="categoryId"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Category</FormLabel>
              <FormControl>
                <Select
                  value={field.value}
                  onValueChange={field.onChange}
                >
                  <SelectTrigger className="bg-zinc-300 w-full">
                    <SelectValue placeholder="Select category" />
                  </SelectTrigger>
                  <SelectContent>
                    {categories.map((cat) => (
                      <SelectItem
                        key={cat.id}
                        value={cat.id.toString()}
                      >
                        {cat.name}
                      </SelectItem>
                    ))}
                  </SelectContent>
                </Select>
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />

        <Button type="submit">
          Update Product
        </Button>
      </form>
    </Form>
  );
}
