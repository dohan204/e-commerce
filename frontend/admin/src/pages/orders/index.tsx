import type { ColumnConfig } from '@/components/TableData'
import TableCustom from '@/components/TableData'
import { Button } from '@/components/ui/button'
import { API_ENDPOINTS } from '@/constants/urls'
import useFetchs from '@/hooks/use-fetch'
import { type OrderResponse } from '@/models/orders'
import { Eye, Octagon, OctagonX, Printer, SquarePen, View } from 'lucide-react'
import React from 'react'
import DetailsOrder from './DetailsOrder'
import cancel from '../../assets/cancel.avif'
import Update from './Update'
export const convertStatus: Record<number, string> = {
  0: 'Đang xử lý',
  1: 'Đã xác nhận',
  2: 'Đang vận chuyển',
  3: 'Đã giao xong',
  4: `Đã hủy`
}  
function Order() {

  const url = API_ENDPOINTS.ORDER.GETALL;
  const {data, loading, refresh} = useFetchs<OrderResponse>(url);

  const dataConvert = data.map(item => ({
    ...item,
    orderCode: item.id + "-" + item.orderCode,
    userId: item.userId.slice(-12),
    status: convertStatus[item.status],
    totalAmount: item.totalAmount.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' }),
    finalAmount: item.finalAmount.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' }),
    createdAt: new Date(item.createdAt).toLocaleDateString('vi-VN'),
    completedAt: item.completedAt === null ? "Chưa Hoàn thành" : new Date(item.completedAt).toLocaleDateString('vi-VN'),
    itemCount: item.items.length
  }))

  console.log(dataConvert)
  const headerTable: ColumnConfig<any>[] = [
    { header: 'Mã đơn', key: 'orderCode' },
    { header: 'Mã người dùng', key: 'userId' },
    { header: 'Số sản phẩm', key: 'itemCount' },
    { header: 'Số tiền thanh toán', key: 'finalAmount' },
    { header: 'Trạng thái', key: 'status' },
    { header: 'Ngày tạo', key: 'createdAt' },
    { header: 'Ngày hoàn thành', key: 'completedAt' },
    { header: 'Thao tác', key: 'action', render: (item) => <div className='flex flex-row gap-2'>
      <DetailsOrder item={item}>
        <Eye />
      </DetailsOrder>
      <Update item={item} refresh={refresh}>
        <SquarePen />
      </Update>
    </div> },
  ]
  return (
    <div className='w-full'>
      <div className='h-20 items-center'>
        <div className='float-start'>
          <h3 className='text-header'>Danh sách đơn hàng</h3>
        </div>
        <div className='float-end'>
          <Button variant={'outline'}>
            <Printer />
            In báo cáo
          </Button>
        </div>
      </div>
      <div>
        <TableCustom columns={headerTable} data={dataConvert} caption='Danh sách đơn hàng' />
      </div>
    </div>
  )
}

export default Order