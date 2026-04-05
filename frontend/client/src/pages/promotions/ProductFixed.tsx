import React from 'react'
import ProductCards from '../categories/ProductCard'
import type { product } from '@/models/Products'
const ProductFixed = ({products, isLoading}: {products?: product[], isLoading: boolean}) => {
  return (
    <div className='w-full px-2'>
        <h3>Sản phẩm giảm giá tới 300k</h3>
        <div className='grid sm:grid-cols-1 md:grid-cols-6 2xl:grid-cols-8 px-2 py-4 gap-2'>
            {products?.map(item => (
                <ProductCards product={item} key={item.id} />
            ))}
        </div>
    </div>
  )
}

export default ProductFixed