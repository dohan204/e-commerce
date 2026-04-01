import React from 'react'
import { useLocation, useNavigate } from 'react-router';

const url = import.meta.env.VITE_API_URL;
const CategoryCard = ({category}: {category: any}) => {
  const navigate = useNavigate();  
  const { pathname } = useLocation();
  return (
    <div className='flex flex-col px-4 py-10 items-center justify-center hover:scale-105 border shadow-md transition'
      onClick={() => navigate(`/categories/${category.slug}`, {state : category.id})}
    >
        <img src={url + category.images}
            className='w-full h-[100px] object-contrai p-2'
        />
        <p>{category.name}</p>
    </div>
  )
}

export default CategoryCard