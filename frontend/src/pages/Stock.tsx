// src/pages/Stock.tsx
import { useState, useEffect, useMemo } from "react";
import { CircularLoader } from "../components/loading";
import { StockCard } from "../components/stockCard";
import { useProducts } from "../hooks/products/useProducts";

import type { Product } from "../services/product.services";
import { useUpdateProductStock } from "@/hooks/products/usePatchProducts";

import {
  Pagination,
  PaginationContent,
  PaginationItem,
  PaginationNext,
  PaginationPrevious,
  PaginationLink,
} from "@/components/ui/pagination";

const PAGE_SIZE = 12;

export const Stock = () => {
  const [page, setPage] = useState(1);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);
  const [savingProductId, setSavingProductId] = useState<string | null>(null);

  const { isLoading, isFetching, data, error } = useProducts({
    page,
    pageSize: PAGE_SIZE,
  });

  const { mutate } = useUpdateProductStock();

  const products: Product[] = data?.data ?? [];
  const totalItems = data?.totalCount ?? 0;
  const totalPages = data?.totalPages ?? 1;

  useEffect(() => {
    // se mudar algum filtro no futuro, pode resetar page aqui
  }, []);

  const pagesToShow = useMemo(() => {
    const pages: number[] = [];
    const start = Math.max(1, page - 2);
    const end = Math.min(totalPages, page + 2);
    for (let p = start; p <= end; p++) {
      pages.push(p);
    }
    return pages;
  }, [page, totalPages]);

  const handlePrev = () => {
    setPage((prev) => Math.max(1, prev - 1));
  };

  const handleNext = () => {
    setPage((prev) =>
      totalPages ? Math.min(totalPages, prev + 1) : prev + 1
    );
  };

  const handleGoToPage = (targetPage: number) => {
    setPage(targetPage);
  };

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
        <h2 className="text-3xl font-semibold text-gray-700">Estoque</h2>
        {isFetching && !isLoading && (
          <span className="text-xs text-gray-500">Atualizando...</span>
        )}
      </div>

      <div className="flex-1 flex flex-col justify-between rounded-lg bg-gray-100 p-3">
        {products.length === 0 ? (
          <div className="flex h-full items-center justify-center rounded-lg bg-white p-6 text-sm text-gray-500">
            Nenhum produto encontrado.
          </div>
        ) : (
          <>
            <div className="grid gap-3 md:grid-cols-2 lg:grid-cols-3">
              {products.map((product) => (
                <StockCard
                  key={product.id}
                  productName={product.name}
                  initialQuantity={product.quantity}
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

            {/* paginação */}
            <div className="mt-4 flex items-center justify-between">
              <span className="text-xs text-gray-500">
                Página {page} de {totalPages} • {totalItems} itens
              </span>

              <Pagination>
                <PaginationContent>
                  <PaginationItem>
                    <PaginationPrevious
                      href="#"
                      onClick={(e) => {
                        e.preventDefault();
                        if (page > 1) handlePrev();
                      }}
                      aria-disabled={page === 1}
                      className={
                        page === 1 ? "pointer-events-none opacity-50" : ""
                      }
                    />
                  </PaginationItem>

                  {pagesToShow.map((p) => (
                    <PaginationItem key={p}>
                      <PaginationLink
                        href="#"
                        isActive={p === page}
                        onClick={(e) => {
                          e.preventDefault();
                          handleGoToPage(p);
                        }}
                      >
                        {p}
                      </PaginationLink>
                    </PaginationItem>
                  ))}

                  <PaginationItem>
                    <PaginationNext
                      href="#"
                      onClick={(e) => {
                        e.preventDefault();
                        if (page < totalPages) handleNext();
                      }}
                      aria-disabled={page === totalPages}
                      className={
                        page === totalPages
                          ? "pointer-events-none opacity-50"
                          : ""
                      }
                    />
                  </PaginationItem>
                </PaginationContent>
              </Pagination>
            </div>
          </>
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
