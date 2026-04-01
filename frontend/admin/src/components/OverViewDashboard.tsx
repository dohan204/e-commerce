import React, { useEffect, useState } from 'react'
import CustomCard from './card-custom'
import { Skeleton } from './ui/skeleton'
import { Package, Warehouse, ShoppingCart, DollarSign, Users, Ticket, Layers } from "lucide-react";
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
                if (!response.ok)
                    throw new Error(response.statusText);
                const data = await response.json();
                setData(data);
                setLoading(false);
            } catch (error) {
                setError(error as Error);
                setLoading(false);
            } finally {
                setLoading(false)
            }
        }
        fetchDate();
    }, [])
    const dataView = [
        { title: 'Tổng sản phẩm', value: data?.data.totalProducts, icon: <Package /> },
        { title: 'Sản phẩm tồn', value: data?.data.totalStock, icon: <Warehouse /> },
        { title: 'Đã bán', value: data?.data.totalSold, icon: <ShoppingCart /> },
        { title: 'Doanh thu', value: data?.data.totalRevenua, icon: <DollarSign /> },
        { title: 'Người dùng', value: 1, icon: <Users /> },
        { title: 'Mã giảm giá', value: 1, icon: <Ticket /> },
        { title: 'Danh mục', value: 8, icon: <Layers /> },
    ]
    const SkeletionLoad = () => (
        Array.from({ length: 8 }).map((_, i) => (
            <div key={i} className='w-full min-h-[140px] p-5 rounded-2xl shadow-md bg-gray-200 animate-pulse flex justify-between items-center'>
                <div className='flex flex-col gap-2'>
                    <Skeleton className='h-4 w-24' />
                    <Skeleton className='h-6 w-16' />
                </div>
                <Skeleton className='h-10 w-10 rounded-full' />
            </div>
        ))
    )
    return (
        <div className='grid sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4'>
            {Loading ? (
                <SkeletionLoad />
            ) : dataView.map((item, i) => (
                <CustomCard title={item.title} value={item.value} key={i} icon={item.icon} />
            ))}
        </div>
    )
}
