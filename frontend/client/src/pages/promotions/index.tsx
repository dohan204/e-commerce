import useFetchData from '@/hooks/useFetch'
import FlastSaleBanner from '../homes/FlastSaleBanner'
import ProductFixed from './ProductFixed'
import type { Base } from '@/models/response/base'
import type { product } from '@/models/Products'
import { API_ENDPOINTS } from '@/constants/UrlGlobal'
import { useState } from 'react'

const Promotion = () => {
  const [page, setPage] = useState(1);
  const rowItems = 20
  const {data, isLoading} = useFetchData<Base<product>>(API_ENDPOINTS.PRODUCT.PAGINATION(page, rowItems), 'products', [page]);
  return (
    <div className='w-full'>
      <div>
        <FlastSaleBanner />
      </div>
      <div>
        <ProductFixed products={data?.data} isLoading={isLoading} />
      </div>
    </div>
  )
}

export default Promotion