import React from 'react'
import { Chart as ChartJS, ArcElement, Tooltip, Legend } from 'chart.js';
import { Pie } from 'react-chartjs-2';
import useFetchSingle from '@/hooks/use-fetchs';
import type { BaseResponse, TopSaleChart } from '@/models/products';
import { API_ENDPOINTS } from '@/constants/urls';

ChartJS.register(ArcElement, Tooltip, Legend);
const TopSale = () => {
    const {data} = useFetchSingle<BaseResponse<TopSaleChart>>(API_ENDPOINTS.PRODUCT.GETTOPSALE);
    const display = {
        labels: data?.data.map(e => e.name),
        datasets: [
            {
                label: '# of Votes',
                data: data?.data.map(e => e.quantity),
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)',
                ],
            },
        ],
    };

    return (
        <div className='flex-1 border-2 rounded-2xl max-w-[350px]'>
            <h4 className='font-light p-2'>Sản phẩm bán chạy nhất</h4>
            <Pie data={display} />
        </div>
    )
}

export default TopSale