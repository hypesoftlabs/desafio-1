// src/hooks/dashboard/useDashboardSummary.ts
import { useQuery } from "@tanstack/react-query";
import { getDashboardSummary } from "../../services/dashboard.services";
import type { DashboardSummary } from "../../services/dashboard.services";

export function useDashboardSummary() {
  return useQuery<DashboardSummary>({
    queryKey: ["dashboard", "summary"],
    queryFn: getDashboardSummary,
  });
}
