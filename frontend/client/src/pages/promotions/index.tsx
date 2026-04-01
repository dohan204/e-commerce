import React from 'react'
import FlastSaleBanner from '../homes/FlastSaleBanner'
import ProductPercent from './ProductPercent'
import ProductFixed from './ProductFixed'

const Promotion = () => {
  return (
    <div className='w-full'>
      <div>
        <FlastSaleBanner />
      </div>
      <div>
        <ProductFixed />
      </div>
    </div>
  )
}

export default Promotion