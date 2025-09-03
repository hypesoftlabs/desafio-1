import { useProducts } from "../../hooks/useProducts";
import { useCategories } from "../../hooks/useCategories";
import {
  Card,
  CardContent,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import { Button } from "@/components/ui/button";

export default function ListProducts() {
  const {
    data: products,
    isLoading,
    error,
  } = useProducts();
  const { data: categories = [] } =
    useCategories(); // pega as categorias

  if (isLoading) return <p>Loading...</p>;
  if (error instanceof Error)
    return <p>Error: {error.message}</p>;

  // função para pegar o nome da categoria pelo id
  const getCategoryName = (id: number) =>
    categories.find((cat) => cat.id === id)
      ?.name || "Unknown";

  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6 p-4">
      {products?.map((product) => (
        <Card
          key={product.id}
          className="hover:shadow-lg transition-shadow"
        >
          <CardHeader>
            <CardTitle className="text-lg font-semibold">
              {product.name}
            </CardTitle>
          </CardHeader>
          <CardContent className="flex flex-col gap-2">
            <p>
              {product.description} -{" "}
              {getCategoryName(
                product.categoryId
              )}
            </p>
            <div className="flex items-center justify-between">
              <Badge variant="secondary">
                R${product.price}
              </Badge>
              <Badge variant="outline">
                Stock: {product.stock}
              </Badge>
            </div>
            <Button
              variant="default"
              size="sm"
              className="mt-2"
            >
              View Details
            </Button>
          </CardContent>
        </Card>
      ))}
    </div>
  );
}
