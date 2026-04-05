import type { Review } from '@/models/Products'
import { User } from 'lucide-react'

const ratingFormat: Record<number, string> = {
    1: '⭐',
    2: '⭐⭐',
    3: '⭐⭐⭐',
    4: '⭐⭐⭐⭐',
    5: '⭐⭐⭐⭐⭐'
}

console.log(ratingFormat[2])
const ReviewDisplay = ({review}: {review: Review}) => {
  return (
    <div className='flex flex-col'>
        <div className='flex flex-row gap-2'>
            <User size={40} />
            <div className='flex flex-col gap-2'>
                <p>{review.userId}</p>
                <p>{ratingFormat[review.rating]}</p>
            </div>
        </div>
        <div className='flex flex-col px-12'>
            <p>{review.comment}</p>
        </div>
    </div>
  )
}

export default ReviewDisplay