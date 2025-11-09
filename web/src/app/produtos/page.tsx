"use client";

import { useEffect, useState } from "react";
import { ProductForm } from "@/components/forms/ProductForm";
import { getProducts, deleteProduct } from "@/services/product";
import { Card } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Trash2 } from "lucide-react";

export default function ProdutosPage() {
  const [products, setProducts] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);

  const loadProducts = async () => {
    try {
      const data = await getProducts();
      setProducts(data);
    } catch (error) {
      console.error("Erro ao carregar produtos:", error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadProducts();
  }, []);

  const handleDelete = async (id: string) => {
    if (!confirm("Tem certeza que deseja excluir este produto?")) return;

    try {
      await deleteProduct(id);
      alert("Produto excluído com sucesso!");
      await loadProducts();
    } catch (error) {
      console.error("Erro ao excluir produto:", error);
      alert("Erro ao excluir produto. Verifique o console para detalhes.");
    }
  };

  if (loading) return <p>Carregando...</p>;

  return (
    <div className="grid md:grid-cols-2 gap-6">
      <Card className="p-6">
        <h2 className="text-xl font-semibold mb-4">Cadastrar Produto</h2>
        <ProductForm onSuccess={loadProducts} />
      </Card>

      <Card className="p-6">
        <h2 className="text-xl font-semibold mb-4">Lista de Produtos</h2>
        {products.length === 0 ? (
          <p className="text-gray-500">Nenhum produto cadastrado.</p>
        ) : (
          <ul className="space-y-3">
            {products.map((p) => (
              <li
                key={p.id}
                className="border rounded p-3 flex justify-between items-center"
              >
                <div>
                  <strong>{p.name}</strong>
                  <p className="text-sm text-gray-500">
                    R$ {p.price} | Estoque: {p.stockQuantity ?? p.stock}
                  </p>
                </div>

                
                <Button
                  variant="destructive"
                  size="sm"
                  onClick={() => handleDelete(p.id)}
                  className="flex items-center gap-2"
                >
                  <Trash2 className="w-4 h-4" />
                  Excluir
                </Button>
              </li>
            ))}
          </ul>
        )}
      </Card>
    </div>
  );
}
