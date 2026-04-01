import {
  Chart as ChartJS,
  ArcElement,
  Tooltip,
  Legend
} from "chart.js";
import { Pie } from "react-chartjs-2";

ChartJS.register(ArcElement, Tooltip, Legend);

const data = {
  labels: ["Laptop", "Phone", "Tablet"],
  datasets: [
    {
      data: [400, 300, 200],
    },
  ],
};

export default function ProductPie() {
  return (
    <div className="bg-white p-4 rounded-xl shadow w-full h-[300px] max-w-[350px]">
      <Pie data={data} />
    </div>
  );
}