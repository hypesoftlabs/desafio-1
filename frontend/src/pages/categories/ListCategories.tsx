import {
  useCategories,
  useDeleteCategory,
} from "@/hooks/useCategories";
import {
  Card,
  CardContent,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Badge } from "@/components/ui/badge";
import { Link } from "react-router-dom";

export default function ListCategories() {
  const {
    data: categories,
    isLoading,
    error,
  } = useCategories();
  const { mutate: deleteCategoryMutate } =
    useDeleteCategory();
  
  if (isLoading) return <p>Loading...</p>;
  if (error instanceof Error)
    return <p>Error: {error.message}</p>;

  if (!categories || categories.length === 0)
    return (
      <p className="p-4 text-center text-gray-500 flex flex-col">
        No categories found
        <Link to="/categories/new" className="text-blue-500 hover:underline">
          Create Category
        </Link>
      </p>
    );

  const handleDelete = (id: number) => {
    if (
      confirm(
        "Are you sure you want to delete this category?"
      )
    ) {
      deleteCategoryMutate(id.toString());
    }
  };

  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6 p-4">
      {categories.map((category) => (
        <Card
          key={category.id}
          className="hover:shadow-lg transition-shadow"
        >
          <CardHeader>
            <CardTitle className="text-lg font-semibold">
              {category.name}
            </CardTitle>
          </CardHeader>
          <CardContent className="flex flex-col gap-2">
            <Badge variant="secondary">
              ID: {category.id}
            </Badge>
            <div className="flex justify-end gap-2 mt-2">
              <Button variant="default" size="sm">
                <Link
                  to={`/categories/${category.id}/update`}
                >
                  Edit Category
                </Link>
              </Button>
              <Button
                variant="destructive"
                size="sm"
                onClick={() =>
                  handleDelete(category.id)
                }
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
