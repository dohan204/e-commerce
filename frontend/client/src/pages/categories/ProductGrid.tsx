import type { product } from '@/models/Products'
import ProductCards from './ProductCard'
import SkeletionCard from '@/components/SkeletionCard'

type Props = {
  products?: product[]
  loading?: boolean
}

const ProductGrid = ({ products , loading }: Props) => {
  if (loading) {
    return (
      <div className='grid sm:grid-cols-1 md:grid-cols-6 2xl:grid-cols-8 gap-4'>
        {Array.from({ length: 24 }).map((_, index) => (
          <SkeletionCard key={index} />
        ))}
      </div>
    )
  }
  return (
    <div className="grid grid-cols-2 md:grid-cols-6 2xl:grid-cols-8 gap-4">
      {products?.map((p) => (
        <ProductCards key={p.id} product={p} />
      ))}
    </div>
  )
}

export default ProductGrid