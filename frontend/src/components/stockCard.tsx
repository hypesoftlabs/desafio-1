
import { useState, useEffect } from "react";
import { Save } from "lucide-react";

type StockCardProps = {
  productName: string;
  initialQuantity: number;
  loading?: boolean;
  onUpdate?: (newQuantity: number) => void;
};

export function StockCard({
  productName,
  initialQuantity,
  loading = false,
  onUpdate,
}: StockCardProps) {
  const [quantity, setQuantity] = useState(initialQuantity);

  useEffect(() => {
    setQuantity(initialQuantity);
  }, [initialQuantity]);

  function handleDecrease() {
    setQuantity((prev) => Math.max(0, prev - 1));
  }

  function handleIncrease() {
    setQuantity((prev) => prev + 1);
  }

  function handleChange(e: React.ChangeEvent<HTMLInputElement>) {
    const value = e.target.value;
    if (value === "") {
      setQuantity(0);
      return;
    }
    const num = Number(value);
    if (!Number.isNaN(num) && num >= 0) {
      setQuantity(num);
    }
  }

  function handleSave() {
    if (!onUpdate) return;
    onUpdate(quantity);
  }

  return (
    <div className="flex flex-col gap-3 rounded-lg bg-white p-4 shadow">
      <h3 className=" text-lg font-semibold text-slate-900">
        {productName}
      </h3>

      <div className="flex items-center gap-3">
        <button
          type="button"
          onClick={handleDecrease}
          disabled={loading || quantity <= 0}
          className="flex h-9 w-9 items-center justify-center rounded-full border border-slate-300 text-lg font-semibold leading-none text-slate-700 hover:bg-slate-100 disabled:cursor-not-allowed disabled:opacity-60"
        >
          -
        </button>

        <input
          type="number"
          min={0}
          value={quantity}
          onChange={handleChange}
          className="w-20 rounded-lg border border-slate-300 px-2 py-1 text-center text-sm text-slate-900"
        />

        <button
          type="button"
          onClick={handleIncrease}
          disabled={loading}
          className="flex h-9 w-9 items-center justify-center rounded-full border border-slate-300 text-lg font-semibold leading-none text-slate-700 hover:bg-slate-100 disabled:cursor-not-allowed disabled:opacity-60"
        >
          +
        </button>
      </div>

      <div className="flex justify-end">
        <button
          type="button"
          onClick={handleSave}
          disabled={loading}
          className="inline-flex items-center gap-1.5 rounded-lg bg-emerald-600 px-4 py-1.5 text-md font-semibold leading-none text-white hover:bg-emerald-700 disabled:cursor-not-allowed disabled:opacity-70"
        >
          <Save className="h-4 w-4 -mt-px" />
          {loading ? "Salvando..." : "Salvar"}
        </button>
      </div>
    </div>
  );
}
