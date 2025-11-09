"use client";

import { useEffect, useState } from "react";
import { DashboardCard } from "@/components/layout/DashboardCard";
import { getProducts, updateProductStock } from "@/services/product";
import { Product } from "@/types/product";
import {
  BarChart,
  Bar,
  XAxis,
  YAxis,
  Tooltip,
  ResponsiveContainer,
  CartesianGrid,
} from "recharts";
import { Button } from "@/components/ui/button";
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogFooter } from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";

export default function DashboardPage() {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedProduct, setSelectedProduct] = useState<Product | null>(null);
  const [newStock, setNewStock] = useState<number>(0);
  const [saving, setSaving] = useState(false);

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    try {
      const data = await getProducts();
      setProducts(data);
    } catch (error) {
      console.error("Erro ao carregar produtos:", error);
    } finally {
      setLoading(false);
    }
  };

  const openModal = (product: Product) => {
    setSelectedProduct(product);
    setNewStock(product.stockQuantity ?? 0);
    setIsModalOpen(true);
  };

  const handleUpdateStock = async () => {
    if (!selectedProduct) return;
    setSaving(true);
    try {
      await updateProductStock(selectedProduct, newStock);
      await loadData();
    } catch (error) {
      console.error("Erro ao atualizar estoque:", error);
    } finally {
      setSaving(false);
      setIsModalOpen(false);
    }
  };

  if (loading) {
    return <p className="text-center mt-10">Carregando dados...</p>;
  }

  const totalProdutos = products.length;
  const valorTotalEstoque = products.reduce(
    (total, p) => total + (p.price * (p.stockQuantity ?? 0)),
    0
  );
  const totalEstoque = products.reduce(
    (total, p) => total + (p.stockQuantity ?? 0),
    0
  );
  const estoqueBaixo = products.filter(
    (p) => (p.stockQuantity ?? 0) < 10
  ).length;

  return (
    <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
      
      <DashboardCard
        title="Total de Produtos"
        value={totalProdutos}
        subtitle="Produtos cadastrados"
      />
      <DashboardCard
        title="Valor em Estoque"
        value={valorTotalEstoque.toLocaleString("pt-BR", {
          style: "currency",
          currency: "BRL",
        })}
        subtitle="Soma total"
      />
      <DashboardCard
        title={estoqueBaixo > 0 ? "Estoque Baixo" : "Estoque Normal"}
        value={totalEstoque}
        subtitle={
          estoqueBaixo > 0
            ? `${estoqueBaixo} produto(s) com menos de 10 unidades`
            : "Todos os produtos com estoque normal"
        }
      />

     
      <div className="bg-white p-6 rounded-2xl shadow md:col-span-3">
        <h2 className="text-xl font-semibold mb-4 text-gray-800">
          Estoque por Produto
        </h2>
        <ResponsiveContainer width="100%" height={350}>
          <BarChart data={products}>
            <CartesianGrid strokeDasharray="3 3" />
            <XAxis dataKey="name" tick={{ fontSize: 12 }} />
            <YAxis />
            <Tooltip />
            <Bar dataKey="stockQuantity" fill="#4F46E5" radius={[8, 8, 0, 0]} />
          </BarChart>
        </ResponsiveContainer>
      </div>

      
      <div className="bg-white p-6 rounded-2xl shadow md:col-span-3">
        <h2 className="text-xl font-semibold mb-4 text-gray-800">
          Gerenciar Estoque
        </h2>
        <table className="w-full border-collapse">
          <thead>
            <tr className="border-b text-left">
              <th className="p-2">Produto</th>
              <th className="p-2">Preço</th>
              <th className="p-2">Estoque</th>
              <th className="p-2 text-right">Ações</th>
            </tr>
          </thead>
          <tbody>
            {products.map((p) => (
              <tr key={p.id} className="border-b hover:bg-gray-50">
                <td className="p-2">{p.name}</td>
                <td className="p-2">
                  {p.price.toLocaleString("pt-BR", {
                    style: "currency",
                    currency: "BRL",
                  })}
                </td>
                <td className="p-2">{p.stockQuantity ?? 0}</td>
                <td className="p-2 text-right">
                  <Button
                    size="sm"
                    onClick={() => openModal(p)}
                    variant="outline"
                  >
                    Atualizar Estoque
                  </Button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      {/* Modal de atualização */}
      {selectedProduct && (
        <Dialog open={isModalOpen} onOpenChange={setIsModalOpen}>
          <DialogContent>
            <DialogHeader>
              <DialogTitle>Atualizar Estoque</DialogTitle>
            </DialogHeader>

            <p className="text-sm text-gray-600 mb-3">
              Produto: <strong>{selectedProduct.name}</strong>
            </p>

            <Input
              type="number"
              value={newStock}
              min={0}
              onChange={(e) => setNewStock(Number(e.target.value))}
            />

            <DialogFooter className="mt-4">
              <Button variant="outline" onClick={() => setIsModalOpen(false)}>
                Cancelar
              </Button>
              <Button onClick={handleUpdateStock} disabled={saving}>
                {saving ? "Salvando..." : "Salvar"}
              </Button>
            </DialogFooter>
          </DialogContent>
        </Dialog>
      )}
    </div>
  );
}
