import {
  Chart as ChartJS,
  LineElement,
  CategoryScale,
  LinearScale,
  PointElement,
  Tooltip,
  Legend
} from "chart.js";
import { Line } from "react-chartjs-2";

ChartJS.register(
  LineElement,
  CategoryScale,
  LinearScale,
  PointElement,
  Tooltip,
  Legend
);

const data = {
  labels: ["T1", "T2", "T3", "T4"],
  datasets: [
    {
      label: "Doanh thu",
      data: [400, 800, 600, 900],
      borderWidth: 2,
      tension: 0.4,
    },
  ],
};

export default function RevenueChart() {
  return (
    <div className="bg-white p-4 rounded-xl shadow w-full h-[300px]">
      <Line data={data} />
    </div>
  );
}