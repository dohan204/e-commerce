import { Eye } from 'lucide-react'
import  { useState } from 'react'

const FlashSaleProductCard = () => {
  const [hover, setHover] = useState<boolean>(false)
  return (
    <div className='flex flex-col px-2 py-6 border rounded-md shadow-xl hover:scale-105 duration-300 transition relative'
      onMouseEnter={() => setHover(true)}
      onMouseLeave={() => setHover(false)}
    >
      <img src={'https://image.celine.com/415a051918fc59eb/original/2V56D896C-38NO_1_WIN22.jpg?im=Resize=(1200);AspectCrop=(1,1),xPosition=.5,yPosition=.5'} className='h-[120px] object-fill px-1' />
      <div className='flex flex-row gap-2'>
        <div>
          <p className="line-through text-gray-400 text-sm">200.000đ</p>
          <p className="text-red-500 font-bold">100.000đ</p>
          <p className='text-sm font-light'>Đã bán: 120</p>
        </div>
        <div className='flex flex-col justify-center items-center'>
          <button className='px-4 py-1 bg-red-300 border rounded-md'>Mua</button>
        </div>
      </div>
      <div className="absolute top-2 left-2 bg-red-500 text-white text-xs px-2 py-1 rounded">
        -50%
      </div>
      {
        hover ? (
          <div className='flex flex-col absolute top-2 right-2 '>
            <Eye className='h-[40px] w-[40px] rounded-full bg-amber-200' />
          </div>
        ) : ''
      }
    </div>
  )
}

export default FlashSaleProductCard