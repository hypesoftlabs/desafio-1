// src/pages/Products.tsx
import { useMemo, useState, useEffect } from "react";
import { Plus, Search } from "lucide-react";

import { ProductCard } from "../components/card";
import { Button } from "../components/button";
import { CircularLoader } from "../components/loading";
import { ProductModal } from "../components/ProductModal";
import { ConfirmModal } from "../components/ConfrmModal";
import { Input } from "../components/input";

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

import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "../components/select";

const PAGE_SIZE = 9;

export const Products = () => {
  const [page, setPage] = useState(1);
  const [search, setSearch] = useState("");
  const [selectedCategory, setSelectedCategory] = useState<string>("all");
  const [modalOpen, setModalOpen] = useState(false);
  const [selectedProduct, setSelectedProduct] = useState<Product | null>(null);
  const [productToDelete, setProductToDelete] = useState<Product | null>(null);

  const categoryIdFilter =
    selectedCategory === "all" ? undefined : selectedCategory;

  useEffect(() => {
    setPage(1);
  }, [search, selectedCategory]);

  const { isLoading, isFetching, data, error } = useProducts({
    page,
    pageSize: PAGE_SIZE,
    name: search || undefined,
    categoryId: categoryIdFilter,
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
  const totalItems = data?.totalCount ?? 0;   // 👈 usando totalCount da API
  const totalPages = data?.totalPages ?? 1;   // 👈 vindo da API também

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
      <h2 className="text-3xl font-semibold text-gray-700">Produtos</h2>

      <div className="mt-2 flex h-20 items-center justify-between gap-5">
        <div className="flex flex-1 items-center gap-3">
          <div className="relative w-full max-w-xs">
            <Search className="pointer-events-none absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 text-gray-400" />
            <Input
              type="text"
              value={search}
              onChange={(e) => setSearch(e.target.value)}
              placeholder="Buscar por nome"
              className="w-full rounded-lg border border-gray-300 py-2 pl-9 pr-3 text-sm"
            />
          </div>

          <Select
            value={selectedCategory}
            onValueChange={(value) => setSelectedCategory(value)}
          >
            <SelectTrigger className="max-w-xs rounded-lg border border-gray-300 py-2 text-md">
              <SelectValue placeholder="Filtrar por categoria" />
            </SelectTrigger>
            <SelectContent>
              <SelectItem value="all">Todas as categorias</SelectItem>
              {categories.map((category) => (
                <SelectItem key={category.id} value={category.id}>
                  {category.name}
                </SelectItem>
              ))}
            </SelectContent>
          </Select>
        </div>

        <Button icon={<Plus />} onClick={handleAdd}>
          Adicionar
        </Button>
      </div>

      <div className="flex-1 flex flex-col place-items-stretch justify-between rounded-lg bg-gray-100 p-3">
        {isFetching && !isLoading && (
          <div className="mb-2 text-xs text-gray-500">Atualizando...</div>
        )}

        {products.length === 0 ? (
          <div className="flex h-full items-center justify-center rounded-lg bg-white p-6 text-sm text-gray-500">
            Nenhum produto encontrado.
          </div>
        ) : (
          <div className="grid gap-3 md:grid-cols-2 lg:grid-cols-3">
            {products.map((product: Product) => {
              const categoryName =
                categoryNameById[product.categoryId] ?? "Sem categoria";

              return (
                <ProductCard
                  key={product.id}
                  product={product}
                  categoryName={categoryName}
                  onEdit={handleEdit}
                  onDelete={handleAskDelete}
                />
              );
            })}
          </div>
        )}

        <div className="mt-4 flex  items-center justify-between">
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
