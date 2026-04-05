import {useEffect, useLayoutEffect, useState } from 'react'
import ProductGrid from './ProductGrid'
import type { Category } from '@/models/Category'
import type { product } from '@/models/Products'
import { API_ENDPOINTS } from '@/constants/UrlGlobal'
import { type Base, type PagedResult } from '@/models/response/base'
import { useLocation } from 'react-router'
import axios from 'axios';
import { useQuery } from '@tanstack/react-query'
import { useReviewStore } from '@/store/useReviewStore'
const CategoryPage = () => {
  const [activeIndex, setActiveIndex] = useState(0)
  const [page, setPage] = useState(1)
  const { state } = useLocation();
  const [categoryId, setCategoryId] = useState<number>(state || 0)
  const [search, setSearch] = useState('')
  const { key } = useReviewStore();
  const rowItems = 24


  // cap nhat lai categoryId khi ma chuyent tu home sang category
  useEffect(() => {
    if (state)
      setCategoryId(state)
    const idx = categories?.data?.findIndex((c) => c.id === state) ?? 0
    setActiveIndex(idx)
  }, [state])
  // const { data: categories } = useFetch<Base<Category>>(API_ENDPOINTS.CATEGORY.GET)
  useLayoutEffect(() => {
    window.scroll(0,0);
  }, [])
  const {data: categories} = useQuery({
    queryKey: ['categories'],
    queryFn: async (): Promise<Base<Category>> => {
      const res = await axios.get(API_ENDPOINTS.CATEGORY.GET)
      return res.data;
    }
  })
  
  const {data: products, isLoading, isError} = useQuery({
    queryKey: ['products', page, rowItems, search, categoryId, key],
    queryFn: async (): Promise<PagedResult<product>> => {
      const res = await axios.get<PagedResult<product>>(API_ENDPOINTS.PRODUCT.PAGINATION(page, rowItems, search, categoryId));
      return res.data;
    },
  })

  const productRender = products?.items || [];
  console.log(categoryId)


  const handleCategoryClick = (index: number, id: number) => {
    setActiveIndex(index)
    setCategoryId(id)
    setPage(1) // reset page khi đổi category
  }

  const totalRecord = products?.total as number
  const totalPages = Math.ceil(totalRecord / rowItems)
  return (
    <div className="w-full bg-white">
      {/* Category bar */}
      <div className="flex gap-3 overflow-x-auto px-4 py-3">
        {categories?.data?.map((item, i) => (
          <button
            key={item.id}
            onClick={() => handleCategoryClick(i, item.id)}
            className={`px-4 py-2 whitespace-nowrap rounded-full border text-sm transition
              ${activeIndex === i ? 'bg-black text-white' : 'hover:bg-black hover:text-white'}
            `}
          >
            {item.name}
          </button>
        ))}
      </div>

      {/* Product Grid */}
      <div className="w-full flex flex-col gap-4 px-4 py-6">
        <ProductGrid products={productRender} loading={isLoading} />

        {productRender && totalRecord > 1 && (
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

export default CategoryPage