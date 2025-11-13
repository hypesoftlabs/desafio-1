import { BarChart3, Package, AlertTriangle } from "lucide-react";
import { CircularLoader } from "../components/loading";

import {
  ResponsiveContainer,
  BarChart,
  Bar,
  XAxis,
  YAxis,
  Tooltip,
} from "recharts";
import { useDashboardGraph } from "@/hooks/dashboard/useGraph";
import { useDashboardSummary } from "@/hooks/dashboard/useSummary";

export const Dashboard = () => {
  const {
    data: summary,
    isLoading: loadingSummary,
    error: summaryError,
  } = useDashboardSummary();

  const {
    data: graphData,
    isLoading: loadingGraph,
    error: graphError,
  } = useDashboardGraph();

  const isLoading = loadingSummary || loadingGraph;
  const error = summaryError || graphError;

  if (isLoading) {
    return (
      <div className="flex h-full items-center justify-center">
        <CircularLoader />
      </div>
    );
  }

  if (error) {
    return (
      <div className="flex h-full items-center justify-center">
        <p className="text-base text-red-500">
          Ocorreu um erro ao carregar o dashboard.
        </p>
      </div>
    );
  }

  if (!summary) return null;

  const lowStorageCount = summary.lowStorageProducts.length;

  return (
    <>
      <div className="flex h-15 items-center justify-between p-2">
        <h2 className="text-3xl font-semibold text-gray-700">
          Dashboard
        </h2>
      </div>

      <div className="flex-1 rounded-2xl bg-gray-100 p-5">
        {/* cards de cima */}
        <div className="mb-5 grid gap-5 md:grid-cols-3">
          {/* Total de produtos */}
          <div className="rounded-2xl bg-white p-5 shadow-sm">
            <div className="mb-4 flex items-center justify-between">
              <span className="text-base font-medium text-gray-500">
                Total de produtos
              </span>
              <div className="flex h-10 w-10 items-center justify-center rounded-full bg-emerald-50">
                <Package className="h-5 w-5 text-emerald-600" />
              </div>
            </div>
            <p className="text-3xl font-semibold text-gray-900">
              {summary.totalProducts}
            </p>
          </div>

          {/* Valor total em estoque */}
          <div className="rounded-2xl bg-white p-5 shadow-sm">
            <div className="mb-4 flex items-center justify-between">
              <span className="text-base font-medium text-gray-500">
                Valor total em estoque
              </span>
              <div className="flex h-10 w-10 items-center justify-center rounded-full bg-emerald-50">
                <BarChart3 className="h-5 w-5 text-emerald-600" />
              </div>
            </div>
            <p className="text-3xl font-semibold text-gray-900">
              R$ {summary.storageValueTotal.toFixed(2)}
            </p>
          </div>

          {/* Produtos com estoque baixo */}
          <div className="rounded-2xl bg-white p-5 shadow-sm">
            <div className="mb-4 flex items-center justify-between">
              <span className="text-base font-medium text-gray-500">
                Produtos com estoque baixo
              </span>
              <div className="flex h-10 w-10 items-center justify-center rounded-full bg-red-50">
                <AlertTriangle className="h-5 w-5 text-red-500" />
              </div>
            </div>
            <p className="text-3xl font-semibold text-gray-900">
              {lowStorageCount}
            </p>
          </div>
        </div>

        {/* gráfico + lista */}
        <div className="grid gap-5 lg:grid-cols-3">
          {/* gráfico de categorias */}
          <div className="rounded-2xl bg-white p-5 shadow-sm lg:col-span-2">
            <div className="mb-4 flex items-center justify-between">
              <div>
                <h3 className="text-base font-semibold text-gray-800">
                  Produtos por categoria
                </h3>
                <p className="text-sm text-gray-500">
                  Quantidade de produtos em cada categoria
                </p>
              </div>
            </div>

            {graphData && graphData.length > 0 ? (
              <div className="mt-4 h-72">
                <ResponsiveContainer width="100%" height="100%">
                  <BarChart data={graphData}>
                    <XAxis
                      dataKey="categoryName"
                      tick={{ fontSize: 12 }}
                      axisLine={false}
                    />
                    <YAxis
                      allowDecimals={false}
                      tick={{ fontSize: 12 }}
                      axisLine={false}
                    />
                    <Tooltip
                      cursor={{ fill: "rgba(15, 23, 42, 0.03)" }}
                      contentStyle={{
                        borderRadius: 12,
                        border: "1px solid #e5e7eb",
                        fontSize: 13,
                      }}
                    />
                    <Bar
                      dataKey="productQuantity"
                      radius={[10, 10, 0, 0]}
                      fill="#22c55e"
                    />
                  </BarChart>
                </ResponsiveContainer>
              </div>
            ) : (
              <div className="flex h-40 items-center justify-center text-base text-gray-500">
                Nenhum dado para exibir.
              </div>
            )}
          </div>

          {/* lista de produtos com estoque baixo */}
          <div className="rounded-2xl bg-white p-5 shadow-sm">
            <h3 className="mb-4 text-base font-semibold text-gray-800">
              Produtos com estoque baixo
            </h3>

            {summary.lowStorageProducts.length === 0 ? (
              <p className="text-base text-gray-500">
                Nenhum produto com estoque baixo.
              </p>
            ) : (
              <ul className="space-y-3 text-sm">
                {summary.lowStorageProducts.map((p) => (
                  <li
                    key={p.id}
                    className="flex items-center justify-between rounded-lg bg-gray-50 px-3 py-2.5"
                  >
                    <div>
                      <p className="text-sm font-medium text-gray-800">
                        {p.name}
                      </p>
                      <p className="text-xs text-gray-500">
                        {p.description}
                      </p>
                    </div>
                    <div className="text-right">
                      <p className="text-xs text-gray-500">Qtd</p>
                      <p className="text-sm font-semibold text-gray-800">
                        {p.quantity}
                      </p>
                    </div>
                  </li>
                ))}
              </ul>
            )}
          </div>
        </div>
      </div>
    </>
  );
};
