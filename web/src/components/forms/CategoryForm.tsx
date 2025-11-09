"use client";

import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { createCategory } from "@/services/categories";
import { useState } from "react";

const schema = z.object({
  name: z.string().min(2, "O nome deve ter pelo menos 2 caracteres"),
});

type CategoryFormData = z.infer<typeof schema>;

export function CategoryForm({ onSuccess }: { onSuccess?: () => void }) {
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors, isSubmitting },
  } = useForm<CategoryFormData>({
    resolver: zodResolver(schema),
  });

  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  const onSubmit = async (data: CategoryFormData) => {
    try {
      await createCategory(data);
      reset();
      onSuccess?.();
      setErrorMessage(null);
    } catch (error: any) {
      setErrorMessage("Erro ao criar categoria. Verifique os dados e tente novamente.");
    }
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
      <div>
        <Input {...register("name")} placeholder="Nome da categoria" />
        {errors.name && (
          <p className="text-sm text-red-500 mt-1">{errors.name.message}</p>
        )}
      </div>

      {errorMessage && (
        <p className="text-sm text-red-600">{errorMessage}</p>
      )}

      <Button type="submit" className="w-full" disabled={isSubmitting}>
        {isSubmitting ? "Salvando..." : "Salvar Categoria"}
      </Button>
    </form>
  );
}
