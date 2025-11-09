import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";

interface DashboardCardProps {
  title: string;
  value: string | number;
  subtitle?: string;
}

export function DashboardCard({ title, value, subtitle }: DashboardCardProps) {
  return (
    <Card className="w-full">
      <CardHeader>
        <CardTitle>{title}</CardTitle>
      </CardHeader>
      <CardContent>
        <p className="text-3xl font-bold">{value}</p>
        {subtitle && <p className="text-sm text-gray-500">{subtitle}</p>}
      </CardContent>
    </Card>
  );
}
