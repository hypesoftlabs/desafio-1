import { DashboardCard } from "@/components/layout/DashboardCard";

export default function DashboardPage() {
  return (
    <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
      <DashboardCard title="Total de Produtos" value={128} subtitle="Produtos cadastrados" />
      <DashboardCard title="Valor em Estoque" value="R$ 52.340,00" subtitle="Soma total" />
      <DashboardCard title="Estoque Baixo" value={7} subtitle="Menos de 10 unidades" />
    </div>
  );
}
