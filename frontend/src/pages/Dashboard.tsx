import { ProductsByCategoryChart } from "@/components/charts/dashboardCharts";
import { useDashboard } from "@/hooks/useDashboard";

export default function Dashboard() {
  const {
    totalProducts,
    totalStockValue,
    lowStock,
    isLoading,
    isError,
  } = useDashboard();

  if (isLoading)
    return (
      <p className="text-center mt-10">
        Loading...
      </p>
    );
  if (isError)
    return (
      <p className="text-center mt-10 text-red-500">
        Error loading dashboard data.
      </p>
    );

  return (
    <div className="p-6 space-y-6">
      <h1 className="text-3xl font-bold mb-4">
        Dashboard
      </h1>

      {/* Metrics cards */}
      <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
        <div className="bg-white dark:bg-gray-800 shadow rounded-lg p-5 text-center">
          <p className="text-gray-500 dark:text-gray-400">
            Total Products
          </p>
          <p className="text-2xl font-semibold">
            {totalProducts}
          </p>
        </div>

        <div className="bg-white dark:bg-gray-800 shadow rounded-lg p-5 text-center">
          <p className="text-gray-500 dark:text-gray-400">
            Total Stock Value
          </p>
          <p className="text-2xl font-semibold">
            ${totalStockValue}
          </p>
        </div>

        <div className="bg-white dark:bg-gray-800 shadow rounded-lg p-5 text-center">
          <p className="text-gray-500 dark:text-gray-400">
            Low Stock Products
          </p>
          <p className="text-2xl font-semibold">
            {lowStock}
          </p>
        </div>
      </div>

      {/* Products by category */}
      <div className="mt-6">
        <h2 className="text-xl font-semibold mb-2">
          Products by Category
        </h2>
        <div className="chart w-full max-h-99">
          <ProductsByCategoryChart />
        </div>
      </div>
    </div>
  );
}
