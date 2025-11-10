// src/pages/Products.tsx
import { useMemo, useState } from "react";
import { Plus } from "lucide-react";

import { ProductCard } from "../components/card";
import { Button } from "../components/button";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "../components/select";
import { CircularLoader } from "../components/loading";
import { ProductModal } from "../components/ProductModal";
import { ConfirmModal } from "../components/ConfrmModal";

import { useProducts } from "../hooks/products/useProducts";
import { useCategories } from "../hooks/categories/useCategories";
import { useDeleteProduct } from "../hooks/products/useDeleteProduct";

import type { Product } from "../services/product.services";
import type { Category } from "../services/category.services";

import {
  Pagination,
  PaginationContent,
  PaginationItem,
  PaginationNext,
  PaginationPrevious,
  PaginationLink,
} from "@/components/ui/pagination";

const PAGE_SIZE = 10;

export const Products = () => {
  const [page, setPage] = useState(1);
  const [modalOpen, setModalOpen] = useState(false);
  const [selectedProduct, setSelectedProduct] = useState<Product | null>(null);
  const [productToDelete, setProductToDelete] = useState<Product | null>(null);

  const { isLoading, isFetching, data, error } = useProducts({
    page,
    pageSize: PAGE_SIZE,
  });

  const { data: categoriesData } = useCategories();
  const categories: Category[] = categoriesData ?? [];

  const deleteMutation = useDeleteProduct();

  const categoryNameById = useMemo(() => {
    const map: Record<string, string> = {};
    categories.forEach((c) => {
      map[c.id] = c.name;
    });
    return map;
  }, [categories]);

  const products = data?.data ?? [];
  const totalItems = data?.totalItems ?? 0;
  const totalPages =
    totalItems > 0 ? Math.ceil(totalItems / PAGE_SIZE) : 1;

  if (isLoading) {
    return (
      <div className="flex h-full items-center justify-center">
        <CircularLoader />
      </div>
    );
  }

  if (error) return <p>{(error as Error).message}</p>;

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

  const pagesToShow = (() => {
    const pages: number[] = [];
    const start = Math.max(1, page - 2);
    const end = Math.min(totalPages, page + 2);
    for (let p = start; p <= end; p++) {
      pages.push(p);
    }
    return pages;
  })();

  function handleAdd() {
    setSelectedProduct(null);
    setModalOpen(true);
  }

  function handleEdit(product: Product) {
    setSelectedProduct(product);
    setModalOpen(true);
  }

  function handleAskDelete(product: Product) {
    setProductToDelete(product);
  }

  function handleConfirmDelete() {
    if (!productToDelete) return;
    deleteMutation.mutate(productToDelete.id, {
      onSuccess: () => setProductToDelete(null),
    });
  }

  function handleCancelDelete() {
    if (deleteMutation.isPending) return;
    setProductToDelete(null);
  }

  return (
    <>
      <div className="flex h-15 justify-between gap-x-5 p-2">
        <h2 className="text-3xl font-semibold text-gray-700">
          Produtos
        </h2>


        <Button icon={<Plus />} onClick={handleAdd}>
          Adicionar
        </Button>
      </div>

      <div className="flex-1 rounded-lg bg-gray-100 p-3">
        {isFetching && !isLoading && (
          <div className="mb-2 text-xs text-gray-500">
            Atualizando...
          </div>
        )}

        {products.length === 0 ? (
          <div className="flex h-full items-center justify-center rounded-lg bg-white p-6 text-sm text-gray-500">
            Nenhum produto encontrado.
          </div>
        ) : (
          <div className="grid gap-3 md:grid-cols-2 lg:grid-cols-3">
            {products.map((product) => {
              const categoryName =
                categoryNameById[product.categoryId] ?? "Sem categoria";

              return (
                <ProductCard
                  key={product.id}
                  product={product}
                  categoryName={categoryName}
                  onEdit={handleEdit}
                  onDelete={handleAskDelete} // <- novo
                />
              );
            })}
          </div>
        )}

        {totalPages > 1 && (
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
        )}
      </div>

      <ProductModal
        open={modalOpen}
        onClose={() => setModalOpen(false)}
        product={selectedProduct}
      />

      <ConfirmModal
        open={!!productToDelete}
        title="Excluir produto"
        message={
          productToDelete
            ? `Tem certeza que deseja excluir "${productToDelete.name}"?`
            : "Tem certeza que deseja excluir este produto?"
        }
        confirmLabel="Excluir"
        cancelLabel="Cancelar"
        loading={deleteMutation.isPending}
        onCancel={handleCancelDelete}
        onConfirm={handleConfirmDelete}
      />
    </>
  );
};
