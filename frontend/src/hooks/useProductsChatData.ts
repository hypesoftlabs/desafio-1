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