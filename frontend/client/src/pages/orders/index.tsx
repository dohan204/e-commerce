import { useUserContext } from '@/hooks/useUserContext'
import { useLocation, useNavigate } from 'react-router'
import useFetch from '@/hooks/useFetch'
import type { Address } from '@/models/Address'
import { API_ENDPOINTS } from '@/constants/UrlGlobal'
import { toast } from 'sonner'
import { useEffect, useState } from 'react'
import { Spinner } from '@/components/ui/spinner'
import type { CartItem } from '@/models/Cart'
import { useCartStore } from '@/store/useCartStore'

const Order = () => {
    const [loading, setLoading] = useState<boolean>(false)
    const location = useLocation()
    const navigate = useNavigate()
    const { user } = useUserContext()
    useEffect(() => {
        window.scrollTo(0, 0)
    }, [])

    const { triggerRefresh } = useCartStore()
    const products = location.state as CartItem[]
    if (!user?.sub) {
        console.log("không thấy mã người dùng")
    }
    const { data: address } = useFetch<Address>(API_ENDPOINTS.ADDRESS.GET(user?.sub))

    if (!products) {
        return <div className="p-10 text-center">Không tìm thấy sản phẩm. <button onClick={() => navigate('/categories')}>Quay lại</button></div>
    }
    const dateOrder = new Date()


    products.forEach(item => {
        console.log(item.productId + ' ' + item.name)
    })
    const handleOrder = async (): Promise<void> => {
        const token = localStorage.getItem('token');
        setLoading(true);
        try {
            const res = await fetch(API_ENDPOINTS.ORDER.CREATE, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`
                },
                body: JSON.stringify({
                    paymentMethod: 1,
                    voucherId: null,
                    note: '',
                    orderItems: products.map(item => ({
                        productId: item.productId,
                        quantity: item.quantity,
                        price: item.price
                    }))
                })
            })
            if (!res.ok)
                throw new Error('Đặt hàng thất bại')

            await Promise.all(
                products.map(item =>
                    fetch(API_ENDPOINTS.CART.DEL(item.productId), {
                        method: 'DELETE',
                        headers: {
                            'Authorization': `Bearer ${token}`
                        }
                    })
                )
            )
            toast.success("Đặt hàng thành công", { position: 'top-center' })
            triggerRefresh();
            navigate('/')
        } catch (err) {
            toast.error("Đặt hàng thất bại")
            console.error(err);
        } finally {
            setLoading(false);
        }
    }

    const totalOrder = products.reduce((acc, item) => {
        return acc + (item.price * item.quantity)
    }, 0)
    return (
        <div className='w-full bg-white min-h-screen p-6'>
            <div className='flex items-center justify-center py-6'>
                <div className='w-full max-w-lg flex flex-col border rounded-md shadow-md p-6 gap-4'>
                    <h3 className='text-xl font-bold text-center'>Thông tin đơn hàng</h3>

                    {/* Thông tin khách hàng */}
                    <div className='flex flex-col gap-2 border-b pb-4'>
                        <p className='font-medium text-gray-500'>Khách hàng</p>
                        <p>Họ tên: <span className='font-semibold'>{user?.FullName}</span></p>
                        <p>Email: <span className='font-semibold'>{user?.email}</span></p>
                        <p>Điện thoại: <span className='font-semibold'>{address?.phone ?? 'Chưa có'}</span></p>
                        <p>Địa chỉ: <span className='font-semibold'>
                            {address ? `${address.ward}, ${address.district}, ${address.province}` : 'Chưa có'}
                        </span></p>
                        <p>Ngày đặt: <span className='font-semibold'>{dateOrder.toLocaleDateString('vi-VN')}</span></p>
                    </div>

                    {/* Chi tiết sản phẩm */}
                    <div className='border-b pb-4'>
                        <p className='font-medium text-gray-500 mb-2'>Chi tiết sản phẩm</p>
                        <table className='w-full text-sm'>
                            <thead>
                                <tr className='bg-gray-50 text-gray-600'>
                                    <th className='text-left px-3 py-2'>Tên sản phẩm</th>
                                    <th className='text-center px-3 py-2'>Số lượng</th>
                                    <th className='text-right px-3 py-2'>Đơn giá</th>
                                </tr>
                            </thead>
                            <tbody>
                                {products.map(item => (
                                    <tr className='border-t' key={item.productId}>
                                        <td className='px-3 py-2 font-semibold'>{item.name}</td>
                                        <td className='px-3 py-2 text-center'>{item.quantity}</td>
                                        <td className='px-3 py-2 text-right'>{item.price}đ</td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                    <div>
                        <p>Tổng tiền: <span className='float-end font-bold'>{totalOrder.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })}</span></p>

                    </div>
                    {/* Nút xác nhận */}
                    <button
                        disabled={loading}
                        className='w-full py-3 bg-orange-500 text-white font-bold rounded-lg hover:bg-orange-600 transition'
                        onClick={handleOrder}
                    >
                        {loading ? (
                            <span className='flex items-center justify-center gap-2'>
                                <Spinner /> Đang xử lý
                            </span>
                        ) : 'Xác nhận đặt hàng'}
                    </button>
                </div>
            </div>
        </div>
    )
}

export default Order