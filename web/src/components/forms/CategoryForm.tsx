"use client";

import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { createCategory } from "@/services/categories";

const schema = z.object({
  name: z.string().min(2),
});

export function CategoryForm({ onSuccess }: { onSuccess?: () => void }) {
  const { register, handleSubmit, reset } = useForm({
    resolver: zodResolver(schema),
  });

  const onSubmit = async (data: any) => {
    await createCategory(data);
    reset();
    onSuccess?.();
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
      <Input {...register("name")} placeholder="Nome da categoria" />
      <Button type="submit" className="w-full">Salvar Categoria</Button>
    </form>
  );
}
