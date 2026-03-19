import { Card, CardContent } from '@/components/ui/card'
import View from './renderview'
import { API_ENDPOINTS } from '@/constants/urls'
import useFetchSingle from '@/hooks/use-fetchs'
import type { BaseResponse, Product, RatingProducts, TopProductsSales } from '@/models/products';
import TopSales from './topsales';
import Rating from './ratings';

const Report = () => {
    const { data } = useFetchSingle<BaseResponse<Product>>(API_ENDPOINTS.PRODUCT.GETALL);
    if (data === null) return;

    const topSales: TopProductsSales[] = data?.data.map((item) => ({
        id: item.id,
        name: item.name,
        price: item.price,
        sold: item.sold
    })) ?? [];

    const ratings: RatingProducts[] = data?.data.map(item => ({
        id: item.id,
        name: item.name,
        description: item.description,
        avgRating: item.avgRating ? item.avgRating : 0,
        reviewCount: item.reviewCount ? item.reviewCount : 0
    }));

    return (
        <div className='w-full'>
            <div className='h-16 bg-amber-50'>
                <h3 className='text-header'>Thống kê sản phẩm</h3>
            </div>
            <div>
                <View data={data} />
            </div>
            <div className='mt-4'>
                <h4>Sản phẩm bán tốt nhất</h4>
                <TopSales data={topSales} />

            </div>
            <div className='mt-4'>
                <h4>Đánh giá sản phẩm</h4>
                <Rating data={ratings} />
            </div>
        </div>
    )
}

export default Report