import React from 'react'
import TopSale from './sales'
import RevenueSale from './revenue'


const Revenue = () => {




  return (
    <div className='w-full'>
      <div className='flex flex-row gap-4 '>
        <RevenueSale />
        <TopSale />
      </div>
    </div>
  )
}

export default Revenue