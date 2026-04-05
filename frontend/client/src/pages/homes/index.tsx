import React, { useLayoutEffect } from 'react'
import CatogoryGrid from './CatogoryGrid'
import FlastSaleBanner from './FlastSaleBanner'
import TopSaleProducts from './TopSaleProducts'
import { useQuery } from '@tanstack/react-query'
import type { Base } from '@/models/response/base'
import type { Category } from '@/models/Category'
import axios from 'axios'
import { API_ENDPOINTS } from '@/constants/UrlGlobal'

const Home = () => {
  useLayoutEffect(() => {
    window.scroll(0,0)
  },[])
  const {data: baseResponse, isLoading} = useQuery({
    queryKey: ['categories'],
    queryFn: async () : Promise<Base<Category>> => {
      const res = await axios.get<Base<Category>>(API_ENDPOINTS.CATEGORY.GET)
      return res.data;
    }
  }) 

  const categories = baseResponse?.data ?? [];
  return (
    <div className='w-full flex flex-col'>
        <div className=''>
          <CatogoryGrid categories={categories} loading={isLoading} />
        </div>
        <div>
          <FlastSaleBanner />
        </div>
        <div>
          <TopSaleProducts />
        </div>
    </div>
  )
}

export default Home