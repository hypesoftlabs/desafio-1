"use client";

import { useState } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogFooter } from "@/components/ui/dialog";

interface UpdateStockModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSave: (newStock: number) => Promise<void>;
  productName: string;
  currentStock: number;
}

export function UpdateStockModal({
  isOpen,
  onClose,
  onSave,
  productName,
  currentStock,
}: UpdateStockModalProps) {
  const [newStock, setNewStock] = useState<number>(currentStock);
  const [loading, setLoading] = useState(false);

  const handleSave = async () => {
    setLoading(true);
    await onSave(newStock);
    setLoading(false);
    onClose();
  };

  return (
    <Dialog open={isOpen} onOpenChange={onClose}>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Atualizar Estoque</DialogTitle>
        </DialogHeader>
        <p className="text-sm text-gray-600 mb-3">
          Produto: <strong>{productName}</strong>
        </p>
        <Input
          type="number"
          value={newStock}
          onChange={(e) => setNewStock(Number(e.target.value))}
          min={0}
        />
        <DialogFooter className="mt-4">
          <Button variant="outline" onClick={onClose}>Cancelar</Button>
          <Button onClick={handleSave} disabled={loading}>
            {loading ? "Salvando..." : "Salvar"}
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
}
