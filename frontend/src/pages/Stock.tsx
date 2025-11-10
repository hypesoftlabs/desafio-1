// src/pages/Stock.tsx
import { useState } from "react";
import { CircularLoader } from "../components/loading";
import { StockCard } from "../components/stockCard";
import { useProducts } from "../hooks/products/useProducts";

import type { Product } from "../services/product.services";
import { useUpdateProductStock } from "@/hooks/products/usePatchProducts";

const PAGE_SIZE = 100;

export const Stock = () => {
  const [page] = useState(1);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);
  const [savingProductId, setSavingProductId] = useState<string | null>(null);

  const { isLoading, isFetching, data, error } = useProducts({
    page,
    pageSize: PAGE_SIZE,
  });

  const { mutate } = useUpdateProductStock();

  const products: Product[] = data?.data ?? [];

  if (isLoading) {
    return (
      <div className="flex h-full items-center justify-center">
        <CircularLoader />
      </div>
    );
  }

  if (error) return <p>{(error as Error).message}</p>;

  return (
    <>
      <div className="flex h-15 justify-between gap-x-5 p-2">
        <h2 className="text-3xl font-semibold text-gray-700">
          Estoque
        </h2>
        {isFetching && !isLoading && (
          <span className="text-xs text-gray-500">
            Atualizando...
          </span>
        )}
      </div>

      <div className="flex-1 rounded-lg bg-gray-100 p-3">
        {products.length === 0 ? (
          <div className="flex h-full items-center justify-center rounded-lg bg-white p-6 text-sm text-gray-500">
            Nenhum produto encontrado.
          </div>
        ) : (
          <div className="grid gap-3 md:grid-cols-2 lg:grid-cols-3">
            {products.map((product) => (
              <StockCard
                key={product.id}
                productName={product.name}
                initialQuantity={product.quantity}
                // só este card fica em loading
                loading={savingProductId === product.id}
                onUpdate={(newQuantity) =>
                  mutate(
                    { id: product.id, quantity: newQuantity },
                    {
                      onMutate: () => {
                        setErrorMessage(null);
                        setSavingProductId(product.id);
                      },
                      onError: () => {
                        setSavingProductId(null);
                        setErrorMessage(
                          "Não foi possível salvar o estoque. Tente novamente."
                        );
                      },
                      onSuccess: () => {
                        setSavingProductId(null);
                      },
                      onSettled: () => {
                        setSavingProductId(null);
                      },
                    }
                  )
                }
              />
            ))}
          </div>
        )}
      </div>

      {errorMessage && (
        <div className="fixed bottom-4 right-4 z-50 max-w-sm rounded-lg border border-red-200 bg-red-50 px-4 py-3 text-sm text-red-700 shadow-lg">
          <div className="flex items-start justify-between gap-3">
            <span>{errorMessage}</span>
            <button
              type="button"
              onClick={() => setErrorMessage(null)}
              className="text-xs font-semibold text-red-700 hover:text-red-900"
            >
              X
            </button>
          </div>
        </div>
      )}
    </>
  );
};
