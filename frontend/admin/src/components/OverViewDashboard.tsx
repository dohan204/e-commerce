import React, { useEffect, useState } from 'react'
import CustomCard from './card-custom'
import { Skeleton } from './ui/skeleton'
// import { title } from 'process'
type data = {
    totalProducts: number,
    totalStock: number,
    totalSold: number,
    totalRevenua: number
}
type response = {
    message: string,
    data: data
}

export default function OverViewDashboard() {
    const [data, setData] = useState<response | null>(null);
    const [Loading, setLoading] = useState<boolean>(true)
    const [error, setError] = useState<Error | null>(null);

    console.log(data);
    useEffect(() => {
        const fetchDate = async () => {
            try {
                const response = await fetch('http://localhost:5255/api/product/dataoverview');
                if(!response.ok) 
                    throw new Error(response.statusText);
                const data = await response.json();
                setData(data);
                setLoading(false);
            } catch(error) {
                setError(error as Error);
                setLoading(false);
            } finally {
                setLoading(false)
            }
        } 
        fetchDate();
    }, [])
    const dataView = [
        {title: 'Tổng số Sản phẩm', details: data?.data.totalProducts},
        {title: 'Sản phẩm tồn', details: data?.data.totalStock},
        {title: 'Sản phẩm đã bán', details: data?.data.totalSold},
        {title: 'Doanh số', details: data?.data.totalRevenua},
        {title: 'Người dùng', details: 1},
        {title: 'Mã giảm giá', details: 1},
        {title: 'Danh mục', details: 8},
        {title: '', details: data?.data.totalRevenua},
    ]
    
    const SkeletionLoad = () => (
        Array.from({length : 8}).map((item, i) => (
            <div key={i} className='w-full min-h-[160px] sm:min-h-[200px] p-6 flex flex-col gap-2 items-center justify-center shadow-xl bg-gray-100 hover:bg-blue-400 rounded-xl transition-all'>
                <Skeleton className='h-8 w-56' />
                <Skeleton className='h-4 w-20' />c
            </div>
        ))
    )

    return (
        <div className='grid sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4'>
            {Loading ? (
                <SkeletionLoad />
            ) : dataView.map((item, i) => (
                <CustomCard key={i}>
                    <h3 className='font-black text-xl'>{item.title}</h3>
                    <p className='font-serif text-red-300'>{item.details}</p>
                </CustomCard>
            ))}
        </div>
    )
}
