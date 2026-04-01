import { API_ENDPOINTS } from '@/constants/UrlGlobal'
import useFetch from '@/hooks/useFetch'
import type { Category } from '@/models/Category'
import React from 'react'
import CategoryCard from './CategoryCard'
import type { Base } from '@/models/response/base'
import SkeletionCard from '@/components/SkeletionCard'

const CatogoryGrid = () => {
    const {data, loading} = useFetch<Base<Category>>(API_ENDPOINTS.CATEGORY.GET);
    
    return (
        <div className='w-full flex flex-col gap-2  border rounded-md px-6 py-4 my-4'>
            <h2 className='text-xl font-black'>Danh mục</h2>
            <div className='bg-white grid grid-cols-1 md:grid-cols-6 2xl:grid-cols-8 gap-2'>
                {loading ? (
                    Array.from({ length: 16 }).map((_, index) => (
                        <SkeletionCard key={index} />
                    ))
                ) : data ? (
                    data.data?.map(item => (
                        <CategoryCard category={item} />
                    ))
                ) : null}
            </div>
        </div>
    )
}

export default CatogoryGrid