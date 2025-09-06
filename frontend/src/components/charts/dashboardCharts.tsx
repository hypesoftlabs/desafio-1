import { Bar } from "react-chartjs-2";
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend,
} from "chart.js";
import { useProductsChartData } from "@/hooks/useDashboard";

ChartJS.register(
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend
);

export function ProductsByCategoryChart() {
  const { labels, dataValues } =
    useProductsChartData();

  const data = {
    labels,
    datasets: [
      {
        label: "Products per Category",
        data: dataValues,
        backgroundColor: "rgba(148, 34, 197, 0.7)", // verde
      },
    ],
  };

  const options = {
    responsive: true,
    plugins: {
      legend: { position: "top" as const },
      title: {
        display: false,
      },
    },
  };

  return <Bar data={data} options={options} />;
}
