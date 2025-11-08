"use client";

import { useEffect, useState } from "react";
import { getProducts } from "@/services/product";
import { Product } from "@/types/product";
import { ProductForm } from "@/components/forms/ProductForm";
import { Card } from "@/components/ui/card";

export default function ProdutosPage() {
  const [products, setProducts] = useState<Product[]>([]);

  const loadProducts = async () => {
    const data = await getProducts();
    setProducts(data);
  };

  useEffect(() => {
    loadProducts();
  }, []);

  return (
    <div className="grid md:grid-cols-2 gap-6">
      <Card className="p-6">
        <h2 className="text-xl font-semibold mb-4">Cadastrar Produto</h2>
        <ProductForm onSuccess={loadProducts} />
      </Card>

      <Card className="p-6">
        <h2 className="text-xl font-semibold mb-4">Lista de Produtos</h2>
        <ul className="space-y-2">
          {products.map((p) => (
            <li key={p.id} className="border rounded p-3">
              <strong>{p.name}</strong> — {p.categoryName || "Sem categoria"}
              <p className="text-sm text-gray-500">R$ {p.price} | Estoque: {p.stock}</p>
            </li>
          ))}
        </ul>
      </Card>
    </div>
  );
}
