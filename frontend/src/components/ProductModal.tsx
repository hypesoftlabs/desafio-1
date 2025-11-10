// src/components/ProductModal.tsx
import React, { useEffect, useState } from "react";
import type { Product } from "../services/product.services";
import type { Category } from "../services/category.services";
import { useCategories } from "../hooks/categories/useCategories";
import { useCreateProduct } from "@/hooks/products/useCreateProduct";
import { useUpdateProduct } from "@/hooks/products/useUpdateProduct";

interface ProductModalProps {
  open: boolean;
  onClose: () => void;
  product?: Product | null;
}

export function ProductModal({ open, onClose, product }: ProductModalProps) {
  const isEdit = !!product;

  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [price, setPrice] = useState<number | "">("");
  const [quantity, setQuantity] = useState<number | "">("");
  const [categoryId, setCategoryId] = useState("");

  const createMutation = useCreateProduct();
  const updateMutation = useUpdateProduct();
  const { data: categoriesData, isLoading: categoriesLoading } = useCategories();

  const categories: Category[] = categoriesData ?? [];

  // sempre que a modal abrir, sincroniza o estado com o produto (ou limpa)
  useEffect(() => {
    if (!open) return;

    if (product) {
      setName(product.name);
      setDescription(product.description);
      setPrice(product.price);
      setQuantity(product.quantity);
      setCategoryId(product.categoryId);
    } else {
      setName("");
      setDescription("");
      setPrice("");
      setQuantity("");
      setCategoryId("");
    }
  }, [open, product]);

  // define categoria padrão somente ao criar
  useEffect(() => {
    if (!open) return;
    if (!isEdit && !categoryId && categories.length > 0) {
      setCategoryId(categories[0].id);
    }
  }, [open, isEdit, categoryId, categories]);

  const isSubmitting = createMutation.isPending || updateMutation.isPending;

  function handleClose() {
    createMutation.reset();
    updateMutation.reset();
    onClose();
  }

  function handleSubmit(e: React.FormEvent) {
    e.preventDefault();

    if (!name.trim() || !description.trim() || !categoryId) return;
    if (price === "" || quantity === "") return;

    const payload = {
      name,
      description,
      price: Number(price),
      quantity: Number(quantity),
      categoryId,
    };

    if (isEdit && product) {
      updateMutation.mutate(
        { id: product.id, ...payload },
        {
          onSuccess: () => handleClose(),
        }
      );
    } else {
      createMutation.mutate(payload, {
        onSuccess: () => handleClose(),
      });
    }
  }

  if (!open) return null;

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/40">
      <div className="w-full max-w-lg rounded-xl bg-white p-6 shadow-xl">
        <div className="mb-4 flex items-center justify-between">
          <h2 className="text-lg font-semibold text-slate-900">
            {isEdit ? "Editar produto" : "Novo produto"}
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
          <div className="grid gap-4 md:grid-cols-2">
            <div className="md:col-span-2">
              <label className="mb-1 block text-sm font-medium text-slate-700">
                Nome
              </label>
              <input
                type="text"
                value={name}
                onChange={(e) => setName(e.target.value)}
                className="w-full rounded-lg border border-slate-300 px-3 py-2 text-sm text-slate-900 outline-none ring-offset-2 transition focus:border-emerald-500 focus:ring-2 focus:ring-emerald-500"
                placeholder="Ex.: Sabonete líquido"
              />
            </div>

            <div className="md:col-span-2">
              <label className="mb-1 block text-sm font-medium text-slate-700">
                Descrição
              </label>
              <textarea
                value={description}
                onChange={(e) => setDescription(e.target.value)}
                className="w-full min-h-[72px] rounded-lg border border-slate-300 px-3 py-2 text-sm text-slate-900 outline-none ring-offset-2 transition focus:border-emerald-500 focus:ring-2 focus:ring-emerald-500"
                placeholder="Descrição do produto"
              />
            </div>

            <div>
              <label className="mb-1 block text-sm font-medium text-slate-700">
                Preço
              </label>
              <input
                type="number"
                step="0.01"
                value={price}
                onChange={(e) =>
                  setPrice(e.target.value === "" ? "" : Number(e.target.value))
                }
                className="w-full rounded-lg border border-slate-300 px-3 py-2 text-sm text-slate-900 outline-none ring-offset-2 transition focus:border-emerald-500 focus:ring-2 focus:ring-emerald-500"
                placeholder="0,00"
              />
            </div>

            <div>
              <label className="mb-1 block text-sm font-medium text-slate-700">
                Estoque
              </label>
              <input
                type="number"
                value={quantity}
                onChange={(e) =>
                  setQuantity(
                    e.target.value === "" ? "" : Number(e.target.value)
                  )
                }
                className="w-full rounded-lg border border-slate-300 px-3 py-2 text-sm text-slate-900 outline-none ring-offset-2 transition focus:border-emerald-500 focus:ring-2 focus:ring-emerald-500"
                placeholder="0"
              />
            </div>

            <div className="md:col-span-2">
              <label className="mb-1 block text-sm font-medium text-slate-700">
                Categoria
              </label>
              <select
                value={categoryId}
                onChange={(e) => setCategoryId(e.target.value)}
                className="w-full rounded-lg border border-slate-300 px-3 py-2 text-sm text-slate-900 outline-none ring-offset-2 transition focus:border-emerald-500 focus:ring-2 focus:ring-emerald-500"
              >
                <option value="">
                  {categoriesLoading
                    ? "Carregando categorias..."
                    : "Selecione uma categoria"}
                </option>
                {categories.map((c) => (
                  <option key={c.id} value={c.id}>
                    {c.name}
                  </option>
                ))}
              </select>
            </div>
          </div>

          {(createMutation.isError || updateMutation.isError) && (
            <p className="text-sm text-red-500">
              Ocorreu um erro ao salvar o produto.
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
                : "Criar produto"}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
