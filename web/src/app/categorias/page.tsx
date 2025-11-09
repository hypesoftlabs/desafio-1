"use client";

import { useEffect, useState } from "react";
import { getCategories } from "@/services/categories";
import { CategoryForm } from "@/components/forms/CategoryForm";
import { Category } from "@/types/category";
import { Card } from "@/components/ui/card";

export default function CategoriasPage() {
  const [categories, setCategories] = useState<Category[]>([]);

  const loadCategories = async () => {
    const data = await getCategories();
    setCategories(data);
  };

  useEffect(() => {
    loadCategories();
  }, []);

  return (
    <div className="grid md:grid-cols-2 gap-6">
      <Card className="p-6">
        <h2 className="text-xl font-semibold mb-4">Cadastrar Categoria</h2>
        <CategoryForm onSuccess={loadCategories} />
      </Card>

      <Card className="p-6">
        <h2 className="text-xl font-semibold mb-4">Categorias Existentes</h2>
        <ul className="space-y-2">
          {categories.map((c) => (
            <li key={c.id} className="border rounded p-3">
              {c.name}
            </li>
          ))}
        </ul>
      </Card>
    </div>
  );
}
