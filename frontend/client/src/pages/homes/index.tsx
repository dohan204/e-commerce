import React from 'react'
import CatogoryGrid from './CatogoryGrid'
import FlastSaleBanner from './FlastSaleBanner'
import TopSaleProducts from './TopSaleProducts'

const Home = () => {
  
  return (
    <div className='w-full'>
        <div className=''>
          <CatogoryGrid />
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