import React from 'react'
import ProductCards from '../categories/ProductCard'
import type { product } from '@/models/Products'
export const products: product[] = [
  {
    id: 1,
    name: "Áo thun basic",
    price: 200000,
    salePrice: 150000,
    avgRatings: 4.3,
    categoryId: 1,
    imageUrl: "https://picsum.photos/200?random=1",
    description: '',
    stock: 10,
    sold: 5,
    reviewCount: 10
  },
  {
    id: 2,
    name: "Quần jean nam",
    price: 500000,
    salePrice: 399000,
    avgRatings: 4.5,
    categoryId: 2,
    imageUrl: "https://picsum.photos/200?random=2",
    description: '',
    stock: 10,
    sold: 5,
    reviewCount: 10
  },
  {
    id: 3,
    name: "Áo hoodie unisex",
    price: 800000,
    salePrice: 599000,
    avgRatings: 4.7,
    categoryId: 1,
    imageUrl: "https://picsum.photos/200?random=3",
    description: '',
    stock: 10,
    sold: 5,
    reviewCount: 10
  },
  {
    id: 4,
    name: "Giày sneaker trắng",
    price: 1200000,
    salePrice: 950000,
    avgRatings: 4.6,
    categoryId: 3,
    imageUrl: "https://picsum.photos/200?random=4",
    description: '',
    stock: 10,
    sold: 5,
    reviewCount: 10
  },
  {
    id: 5,
    name: "Balo thời trang",
    price: 450000,
    salePrice: 350000,
    avgRatings: 4.2,
    categoryId: 4,
    imageUrl: "https://picsum.photos/200?random=5",
    description: '',
    stock: 10,
    sold: 5,
    reviewCount: 10
  },
  {
    id: 6,
    name: "Áo sơ mi công sở",
    price: 350000,
    salePrice: 299000,
    avgRatings: 4.4,
    categoryId: 1,
    imageUrl: "https://picsum.photos/200?random=6",
    description: '',
    stock: 10,
    sold: 5,
    reviewCount: 10
  },
  {
    id: 7,
    name: "Quần short thể thao",
    price: 250000,
    salePrice: 199000,
    avgRatings: 4.1,
    categoryId: 2,
    imageUrl: "https://picsum.photos/200?random=7",
    description: '',
    stock: 10,
    sold: 5,
    reviewCount: 10
  },
  {
    id: 8,
    name: "Dép sandal",
    price: 300000,
    salePrice: 230000,
    avgRatings: 4.0,
    categoryId: 3,
    imageUrl: "https://picsum.photos/200?random=8",
    description: '',
    stock: 10,
    sold: 5,
    reviewCount: 10
  },
  {
    id: 9,
    name: "Mũ lưỡi trai",
    price: 150000,
    salePrice: 99000,
    avgRatings: 3.9,
    categoryId: 4,
    imageUrl: "https://picsum.photos/200?random=9",
    description: '',
    stock: 10,
    sold: 5,
    reviewCount: 10
  },
  {
    id: 10,
    name: "Túi đeo chéo",
    price: 400000,
    salePrice: 320000,
    avgRatings: 4.3,
    categoryId: 4,
    imageUrl: "https://picsum.photos/200?random=10",
    description: '',
    stock: 10,
    sold: 5,
    reviewCount: 10
  }
];
const ProductFixed = () => {
  return (
    <div className='w-full px-2'>
        <h3>Sản phẩm giảm giá tới 300k</h3>
        <div className='grid sm:grid-cols-1 md:grid-cols-6 2xl:grid-cols-8 px-2 py-4 gap-2'>
            {products.map(item => (
                <ProductCards product={item} />
            ))}
        </div>
    </div>
  )
}

export default ProductFixed