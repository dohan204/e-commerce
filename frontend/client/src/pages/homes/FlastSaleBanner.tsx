import React from 'react'
import FlashSaleProductCard from './FlashSaleProductCard'

const FlastSaleBanner = () => {
  return (
    <div className='flex flex-col px-6 py-6 gap-2 bg-white'>
        <div>
            <h3 className='text-2xl font-extrabold text-shadow-red-400 text-red-200'>Flash Sale</h3>
        </div>
        <div className='grid grid-cols-1 md:grid-cols-6 2xl:grid-cols-8 py-2 gap-2'>
            {Array.from({length: 9}).map((item, i) => (
                <FlashSaleProductCard key={i} />
            ))}
        </div>
    </div>
  )
}

export default FlastSaleBanner