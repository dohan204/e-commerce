import { API_ENDPOINTS } from '@/constants/UrlGlobal';
import useFetch from '@/hooks/useFetch';
import type { PagedResult } from '@/models/response/base';
import ProductCards from '@/pages/categories/ProductCard';
import { useState } from 'react';
import { useSearchParams } from 'react-router'

const SearchResult = () => {
    const [page, setPage] = useState(1);
    const rowPages = 16
    const [searchParams] = useSearchParams();
    const q = searchParams.get('q');
    console.log('query: ', q)
    // call api 
    const { data } = useFetch<PagedResult<any>>(API_ENDPOINTS.PRODUCT.PAGINATION(page, rowPages, q ?? ''), [q])
    console.log(data);
    if (!q)
        return null;

    const totalRecord = data?.total as number
    const totalPages = Math.ceil(totalRecord / rowPages)
    return (
        <div className='w-full bg-white'>
            <div className='flex flex-col px-6 py-6'>
                <h2 className='text-2xl font-stretch-75%'>Kết quả tìm kiếm</h2>
                <div className='grid grid-cols-1 md:grid-cols-6 2xl:grid-cols-8 px-4 py-6 gap-2'>
                    {!data ? (<div className='flex items-center justify-center'>
                        <p className='text-xl font-black italic'>Không tìm thấy sản phẩm</p>
                    </div>) : data?.items.map(item => (
                        <ProductCards key={item.id} product={item} />
                    ))}
                </div>
                {data && totalRecord > 1 && (
          <div className="flex justify-center gap-2 mt-4">
            <button
              disabled={page === 1}
              onClick={() => setPage((p) => p - 1)}
              className="px-4 py-2 border rounded disabled:opacity-40"
            >
              Trước
            </button>
            <span className="px-4 py-2 text-sm">
                {page} / {totalPages}
            </span>
            <button
              disabled={page === totalPages}
              onClick={() => setPage((p) => p + 1)}
              className="px-4 py-2 border rounded disabled:opacity-40"
            >
              Sau
            </button>
          </div>
        )}
            </div>
        </div>
    )
}

export default SearchResult