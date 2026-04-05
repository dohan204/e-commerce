import ProductCards from '../categories/ProductCard'
import type { product } from '@/models/Products'
import type { Base } from '@/models/response/base'
import { API_ENDPOINTS } from '@/constants/UrlGlobal'
import SkeletionCard from '@/components/SkeletionCard'
import useFetchData from '@/hooks/useFetch'

const TopSaleProducts = () => {
    const {data, isLoading} = useFetchData<Base<product>>(API_ENDPOINTS.PRODUCT.GETSALES, 'productsSales')
  return (
    <div className='bg-white w-full border rounded-md px-4 py-6'>
        <h3>Sản Phẩm bán top đầu</h3>
        <div className='grid sm:grid-cols-1 md:grid-cols-6 2xl:grid-cols-8 gap-4 '>
            {isLoading ? Array.from({length: 16}).map((_, index) => (
              <SkeletionCard key={index} />
            )) : data?.data?.map(p => (
              <ProductCards product={p} key={p.id} />
            ))}
        </div>
    </div>
  )
}

export default TopSaleProducts