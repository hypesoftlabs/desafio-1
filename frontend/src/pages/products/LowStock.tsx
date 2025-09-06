import { useDashboard } from "@/hooks/useDashboard";
import {
  Card,
  CardContent,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";

type Product = {
  id: number;
  name: string;
  stock: number;
};

const LowStock = () => {
  const { lowStock: lowStockQuery } =
    useDashboard();

  return (
    <Card className="w-full max-w-md mx-auto mt-5">
      <CardHeader>
        <CardTitle>Low Stock Products</CardTitle>
      </CardHeader>
      <CardContent className="space-y-3">
        {Array.isArray(lowStockQuery) &&
        lowStockQuery.length > 0 ? (
          <ul className="flex flex-col gap-2">
            {lowStockQuery.map(
              (product: Product) => (
                <li
                  key={product.id}
                  className="flex justify-between items-center p-3 bg-gray-50 dark:bg-gray-800 rounded-lg shadow-sm hover:shadow-md transition-shadow"
                >
                  <span>{product.name}</span>
                  <Badge
                    variant={
                      product.stock <= 5
                        ? "destructive"
                        : "secondary"
                    }
                    className="text-sm"
                  >
                    {product.stock} in stock
                  </Badge>
                </li>
              )
            )}
          </ul>
        ) : (
          <p className="text-gray-500 dark:text-gray-400">
            No low stock products found.
          </p>
        )}
      </CardContent>
    </Card>
  );
};

export default LowStock;
