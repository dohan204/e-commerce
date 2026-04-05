import type { product, Review } from '@/models/Products'
import { useNavigate } from 'react-router'
import { url } from '@/constants/UrlGlobal'

type Props = {
  product: product
}

const ProductCards = ({ product }: Props) => {
  const navigate = useNavigate()

  const discountPercent =
    product.salePrice && product.price > 0
      ? Math.round((1 - product.salePrice / product.price) * 100)
      : null

  if(!product.reviews) {
    return;
  } 
  const avg = CalculatorRating(product.reviews);
    const prd = {
      ...product,
      avgRatings: avg
    }
  return (
    <div
      className="relative border rounded-xl p-4 shadow hover:scale-105 transition cursor-pointer"
      onClick={() => navigate(`/product/detail/${product.id}`, { state: prd })}
    >
      {/* Discount badge */}
      {discountPercent !== null && (
        <span className="absolute top-2 left-2 bg-red-500 text-white text-xs px-2 py-1 rounded z-10">
          -{discountPercent}%
        </span>
      )}

      {/* Image */}
      <img
        src={url + product.imageUrl}
        alt={product.name}
        className="w-full h-[120px] object-contain rounded-md"
      />

      {/* Info */}
      <div className="mt-3">
        <div className="flex items-center gap-1 text-yellow-500 text-sm">
          ⭐ <span className="text-black">{prd.avgRatings}</span>
        </div>

        <h3 className="font-medium text-sm mt-1 line-clamp-2">{product.name}</h3>

        <div className="flex items-center gap-2 mt-1 flex-wrap">
          <span className="text-red-500 font-bold text-md">
            {(product.salePrice ? product.salePrice : product.price).toLocaleString('vi-VN', { style: 'currency', currency: 'VND'})}
            {product.salePrice ? product.salePrice && <span className='line-through opacity-40 p-2'>{product.price.toLocaleString('vi-VN', {style: 'currency', currency: 'VND'})}</span> : null}
          </span>
        </div>
      </div>
    </div>
  )
}

export default ProductCards

function CalculatorRating(ratings: Review[]) {
    return ratings.length === 0
        ? 0
        : ratings.reduce((sum, item) => sum + item.rating, 0) / ratings.length;
}