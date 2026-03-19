// import React from 'react'
import { Card, CardContent, CardDescription } from '@/components/ui/card';
// import type { res } from '../products'
import { API_ENDPOINTS } from '@/constants/urls'
import useFetchSingle from '@/hooks/use-fetchs'
import type { BaseResponse, Product, TopProductsSales } from '@/models/products';
import TopSales from './topsales';
export default function View({data}: {data: BaseResponse<Product>}) {

    const totalProducts = data?.data.length;
    const stockProducts = data?.data.reduce((item, cal) => item + cal.stock, 0)
    const soldProducts = data?.data.reduce((item, cal) => item + cal.sold, 0)

    const view = [
        { label: 'Tổng số sản phẩm', value: totalProducts },
        { label: 'Số sản phẩm tồn kho', value: stockProducts },
        { label: 'Số sản phẩm đã bán', value: soldProducts },
    ]

    return (
        <div>
            <div className='grid sm:grid-cols-1 md:grid-cols-3 gap-4'>
                {view.map((item, i) => (
                    <Card key={i} className='flex items-center p-4'>
                        <CardContent>{item.label}</CardContent>
                        <CardDescription>{item.value}</CardDescription>
                    </Card>
                ))}
            </div>
        </div>
    )
}
