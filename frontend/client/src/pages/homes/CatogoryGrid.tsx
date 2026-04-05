import type { Category } from '@/models/Category'
import CategoryCard from './CategoryCard'
import SkeletionCard from '@/components/SkeletionCard'

const CatogoryGrid = ({categories, loading}: {categories: Category[], loading: boolean}) => {    
    return (
        <div className='w-full flex flex-col gap-2  border rounded-md px-6 py-4 my-4'>
            <h2 className='text-xl font-black'>Danh mục</h2>
            <div className='bg-white grid grid-cols-1 md:grid-cols-6 2xl:grid-cols-8 gap-2'>
                {loading ? (
                    Array.from({ length: 16 }).map((_, index) => (
                        <SkeletionCard key={index} />
                    ))
                ) : categories ? (
                    categories.map(item => (
                        <CategoryCard category={item} key={item.id} />
                    ))
                ) : <div className='w-screen flex justify-center items-center'>Không có dữ liệu về Danh mục</div>}
            </div>
        </div>
    )
}

export default CatogoryGrid