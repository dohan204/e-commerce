import React from 'react'
import { Bar } from "react-chartjs-2";
import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    BarElement,
    Tooltip,
    Legend
} from "chart.js"
import type { BaseResponse } from '@/models/products';
import type { Revence } from '@/models/orders';
import { API_ENDPOINTS } from '@/constants/urls';
import { useQuery } from '@tanstack/react-query';


ChartJS.register(
    CategoryScale,
    LinearScale,
    BarElement,
    Tooltip,
    Legend
)
const RevenueSale = () => {
    const { data } = useQuery({
        queryKey: ['revenuas'],
        queryFn: async (): Promise<BaseResponse<Revence>> => {
            const res = await fetch(API_ENDPOINTS.ORDER.GETREVENUE);
            return await res.json();
        }
    })

    const datas = {
        labels: data?.data.map(e => new Date(e.date).toLocaleDateString('vi-VN')),
        datasets: [
            {
                label: "Doanh thu",
                data: data?.data.map(e => e.value),
                backgroundColor: 'rgba(53, 162, 235, 0.5)'
            }
        ],
    };
    const options = {
        responsive: true
    }
    return (
        <div className='flex-1 border-2 rounded-2xl'>
            <h4 className='font-light p-2'>Doanh số bán hàng</h4>
            <Bar data={datas} options={options} />
        </div>
    )
}

export default RevenueSale