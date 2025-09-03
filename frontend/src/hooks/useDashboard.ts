import { useQuery } from "@tanstack/react-query";
import {
  getTotalProducts,
  getProductsByCategory,
  getTotalStockValue,
  getLowStock,
} from "@/services/dashboard";

export const useDashboard = () => {
  const totalProductsQuery = useQuery({
    queryKey: ["totalProducts"],
    queryFn: getTotalProducts
  });
  const totalStockValueQuery = useQuery({
    queryKey: ["totalStockValue"],
    queryFn: getTotalStockValue
  });
  const lowStockQuery = useQuery({
    queryKey: ["lowStock"],
    queryFn: getLowStock
  });
  const productsByCategoryQuery = useQuery({
    queryKey: ["productsByCategory"],
    queryFn: getProductsByCategory
  });

  return {
    totalProducts: totalProductsQuery.data,
    totalStockValue: totalStockValueQuery.data,
    lowStock: lowStockQuery.data,
    productsByCategory: productsByCategoryQuery.data,
    isLoading:
      totalProductsQuery.isLoading ||
      totalStockValueQuery.isLoading ||
      lowStockQuery.isLoading ||
      productsByCategoryQuery.isLoading,
    isError:
      totalProductsQuery.isError ||
      totalStockValueQuery.isError ||
      lowStockQuery.isError ||
      productsByCategoryQuery.isError,
  };
};

import { useProducts } from "@/hooks/useProducts";
import { useCategories } from "@/hooks/useCategories";

export function useProductsChartData() {
  const { data: products = [] } = useProducts();
  const { data: categories = [] } = useCategories();

  const labels = categories.map((cat) => cat.name);
  const dataValues = categories.map((cat) => {
    return products.filter((p) => p.categoryId === cat.id).length;
  });

  return { labels, dataValues };
}