"use client";

import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { createProduct } from "@/services/product";
import { getCategories } from "@/services/categories";

const schema = z.object({
  name: z.string().min(1, "Nome é obrigatório"),
  price: z.coerce.number().min(0, "Preço deve ser maior ou igual a 0"),
  stock: z.coerce.number().min(0, "Estoque deve ser maior ou igual a 0"),
  categoryId: z.string().min(1, "Categoria é obrigatória"),
  description: z.string().optional().default(""),
});

export type ProductFormData = z.infer<typeof schema>;

interface Category {
  id: string;
  name: string;
}

interface ProductFormProps {
  onSuccess?: () => void;
}

export function ProductForm({ onSuccess }: ProductFormProps) {
  const [categories, setCategories] = useState<Category[]>([]);

  const { register, handleSubmit, reset } = useForm<z.infer<typeof schema>>({
  resolver: zodResolver(schema) as any, // força o resolver tipado corretamente
});

  useEffect(() => {
    getCategories().then((data: Category[]) => {
      // força tipagem e ignora categorias com id indefinido
      setCategories(data.filter((c) => !!c.id));
    });
  }, []);

  const onSubmit = async (data: ProductFormData) => {
    await createProduct({
      ...data,
      description: data.description ?? "", // garante string
    });
    reset();
    onSuccess?.();
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
      <Input {...register("name")} placeholder="Nome do produto" />
      <Input {...register("description")} placeholder="Descrição" />
      <Input {...register("price")} placeholder="Preço" type="number" />
      <Input {...register("stock")} placeholder="Estoque" type="number" />
      <select {...register("categoryId")} className="w-full border rounded p-2">
        <option value="">Selecione uma categoria</option>
        {categories.map((c) => (
          <option key={c.id} value={c.id}>
            {c.name}
          </option>
        ))}
      </select>
      <Button type="submit" className="w-full">
        Salvar Produto
      </Button>
    </form>
  );
}
