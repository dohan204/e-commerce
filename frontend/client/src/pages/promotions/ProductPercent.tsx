import React from 'react'

const ProductPercent = () => {
  return (
    <div className='w-full'>
        <h3>Sản phẩm giảm giá từ 10 - 59%</h3>
        <div className='grid sm:grid-cols-1 md:grid-cols-6 px-2 py-4 gap-4'>
            {Array.from({length: 12}).map((item, i) => (
                <div className='flex justify-center items-center px-10 py-12 border rounded-md shadow'>
                    Sản phẩm {i}
                </div>
            ))}
        </div>
    </div>
  )
}

export default ProductPercent