// src/components/CategoryModal.tsx
import React, { useEffect, useState } from "react";
import { useCreateCategory } from "../hooks/categories/useCreateCategories";
import { useUpdateCategory } from "../hooks/categories/useUpdateCategories";
import type { Category } from "../services/category.services";

interface CategoryModalProps {
  open: boolean;
  onClose: () => void;
  category?: Category | null;
}

export function CategoryModal({ open, onClose, category }: CategoryModalProps) {
  const isEdit = !!category;
  const [name, setName] = useState("");

  const createMutation = useCreateCategory();
  const updateMutation = useUpdateCategory();

  useEffect(() => {
    setName(category?.name ?? "");
  }, [category]);

  const isSubmitting = createMutation.isPending || updateMutation.isPending;

  function handleClose() {
    setName("");
    createMutation.reset();
    updateMutation.reset();
    onClose();
  }

  function handleSubmit(e: React.FormEvent) {
    e.preventDefault();

    if (!name.trim()) return;

    if (isEdit && category) {
      updateMutation.mutate(
        { id: category.id, name },
        {
          onSuccess: () => {
            handleClose();
          },
        }
      );
    } else {
      createMutation.mutate(
        { name },
        {
          onSuccess: () => {
            handleClose();
          },
        }
      );
    }
  }

  if (!open) return null;

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/40">
      <div className="w-full max-w-md rounded-xl bg-white p-6 shadow-xl">
        <div className="mb-4 flex items-center justify-between">
          <h2 className="text-lg font-semibold text-slate-900">
            {isEdit ? "Editar categoria" : "Nova categoria"}
          </h2>
          <button
            type="button"
            className="rounded-full p-1 text-slate-500 hover:bg-slate-100 hover:text-slate-800"
            onClick={handleClose}
          >
            ✕
          </button>
        </div>

        <form onSubmit={handleSubmit} className="space-y-4">
          <div>
            <label className="mb-1 block text-sm font-medium text-slate-700">
              Nome da categoria
            </label>
            <input
              type="text"
              value={name}
              onChange={(e) => setName(e.target.value)}
              className="w-full rounded-lg border border-slate-300 px-3 py-2 text-sm text-slate-900 outline-none ring-offset-2 transition focus:border-emerald-500 focus:ring-2 focus:ring-emerald-500"
              placeholder="Ex.: Eletrônicos"
            />
          </div>

          {(createMutation.isError || updateMutation.isError) && (
            <p className="text-sm text-red-500">
              Ocorreu um erro ao salvar a categoria.
            </p>
          )}

          <div className="flex justify-end gap-2 pt-2">
            <button
              type="button"
              onClick={handleClose}
              className="rounded-lg border border-slate-300 px-4 py-2 text-sm font-medium text-slate-700 hover:bg-slate-100"
            >
              Cancelar
            </button>
            <button
              type="submit"
              disabled={isSubmitting}
              className="rounded-lg bg-emerald-600 px-4 py-2 text-sm font-semibold text-white shadow hover:bg-emerald-700 disabled:cursor-not-allowed disabled:opacity-70"
            >
              {isSubmitting
                ? "Salvando..."
                : isEdit
                ? "Salvar alterações"
                : "Criar categoria"}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
