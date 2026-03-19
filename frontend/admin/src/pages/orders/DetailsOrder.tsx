import { Button } from '@/components/ui/button'
import { Dialog, DialogClose, DialogContent, DialogDescription, DialogFooter, DialogHeader, DialogTitle, DialogTrigger } from '@/components/ui/dialog'
import { Label } from '@/components/ui/label'
import useFetchSingle from '@/hooks/use-fetchs'
import type { OrderResponse } from '@/models/orders'
import React, { type ReactNode } from 'react'
import type { res } from '../products'
import { API_ENDPOINTS } from '@/constants/urls'


const DetailsOrder = ({ item, children }: { item: OrderResponse, children: ReactNode }) => {
    const { data } = useFetchSingle<res>(API_ENDPOINTS.PRODUCT.GETALL);
    const productMap = Object.fromEntries(
        data?.data.map(p => [p.id, p.name]) || []
    );

    return (
        <Dialog>
            <DialogTrigger asChild>
                {children}
            </DialogTrigger>
            <DialogContent>
                <DialogHeader>
                    <DialogTitle>
                        Chi tiết Thông tin đơn hàng
                    </DialogTitle>
                    <DialogDescription className='text-medium'>
                        {item.orderCode}
                    </DialogDescription>
                </DialogHeader>
                <div className='flex flex-col gap-2'>
                    <Label>Mã người dùng: {item.userId}</Label>
                    <Label>Số mặt hàng: {item.items.length}  </Label>
                    {Array.isArray(item.items) && item.items.map((value) => (
                        <div className='grid grid-cols-2 items-center'>
                            <p className='text-xs'>{productMap[value.productId]}</p>
                            <div className='flex flex-col justify-center'>
                                <p className='text-xs'>x{value.quantity}</p>
                                <p className='text-xs'>{value.price}</p>
                            </div>
                        </div>
                    ))}
                    <Label>Tổng giá trị đơn hàng: {item.totalAmount}</Label>
                    <Label>Phí giao hàng: {item.shippingFee}</Label>
                    <Label>Mã giảm giá: {item.voucherId}</Label>
                    <Label>Số tiền giảm: {item.discountAmount}</Label>
                    <Label>Số tiền phải trả: {item.finalAmount}</Label>
                    <Label>Ngày tạo: {new Date(item.createdAt).toLocaleDateString('vi-VN')}</Label>
                </div>

                <DialogFooter>
                    <DialogClose asChild>
                        <Button variant={'outline'}>
                            Đóng
                        </Button>
                    </DialogClose>
                </DialogFooter>
            </DialogContent>
        </Dialog>
    )
}

export default DetailsOrder