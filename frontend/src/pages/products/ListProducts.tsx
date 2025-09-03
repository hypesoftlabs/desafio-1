import {
  useDeleteProduct,
  useProducts,
} from "../../hooks/useProducts";
import { useCategories } from "../../hooks/useCategories";
import {
  Card,
  CardContent,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import { Button } from "@/components/ui/button";
import { Link } from "react-router-dom";

export default function ListProducts() {
  const {
    data: products,
    isLoading,
    error,
  } = useProducts();
  const { data: categories = [] } =
    useCategories();

  const { mutate: deleteProductMutate } =
    useDeleteProduct();

  if (isLoading) return <p>Loading...</p>;
  if (error instanceof Error)
    return <p>Error: {error.message}</p>;

  if (!products || products.length === 0)
    return (
      <p className="p-4 text-center text-gray-500 flex flex-col">
        No products found
        <Link
          to="/products/new"
          className="text-blue-500 hover:underline"
        >
          Create Product
        </Link>
      </p>
    );

  // função para pegar o nome da categoria pelo id
  const getCategoryName = (id: number) =>
    categories.find((cat) => cat.id === id)
      ?.name || "No category assigned";

  const handleDelete = (id: number) => {
    if (
      confirm(
        "Are you sure you want to delete this product?"
      )
    ) {
      deleteProductMutate(id.toString());
    }
  };

  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6 p-4">
      {products.map((product) => (
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
            <div className="flex justify-end gap-2 mt-2">
              <Button variant="default" size="sm">
                Edit Product
              </Button>
              <Button
                variant="destructive"
                size="sm"
                onClick={() => {
                  if (product.id !== undefined) {
                    handleDelete(product.id);
                  }
                }}
              >
                Delete
              </Button>
            </div>
          </CardContent>
        </Card>
      ))}
    </div>
  );
}
