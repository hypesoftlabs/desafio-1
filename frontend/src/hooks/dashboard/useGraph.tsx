// src/hooks/dashboard/useDashboardGraph.ts
import { useQuery } from "@tanstack/react-query";
import { getDashboardGraph } from "../../services/dashboard.services";
import type { DashboardCategoryGraphItem } from "../../services/dashboard.services";

export function useDashboardGraph() {
  return useQuery<DashboardCategoryGraphItem[]>({
    queryKey: ["dashboard", "graph"],
    queryFn: getDashboardGraph,
  });
}
