import InformationContact from './InformationContact'
import FormContact from './FormContact'
import { Clock } from 'lucide-react'

const Contact = () => {
    return (
        <div className='w-full'>
            {/* banner / header */}
            <div className='bg-gradient-to-r from-blue-500 to-cyan-400 text-white py-10 text-center'>
                <h2 className='text-3xl font-bold mb-2'>Liên hệ với chúng tôi</h2>
                <p className='text-sm opacity-90'>
                    Nếu bạn có bất kỳ câu hỏi nào, hãy gửi cho chúng tôi
                </p>
            </div>

            {/* working time */}
            <div className='flex items-center justify-center gap-2 py-6 text-gray-600'>
                <Clock className='text-blue-500' />
                <span>
                    Thời gian làm việc: <strong>8h - 23h</strong>
                </span>
            </div>

            {/* main content */}
            <div className='px-4 pb-10 grid sm:grid-cols-1 md:grid-cols-2 gap-6 max-w-6xl mx-auto'>
                <InformationContact />
                <FormContact />
            </div>

        </div>
    )
}

export default Contact