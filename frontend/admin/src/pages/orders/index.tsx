import type { ColumnConfig } from '@/components/TableData'
import TableCustom from '@/components/TableData'
import { Button } from '@/components/ui/button'
import { API_ENDPOINTS } from '@/constants/urls'
import { type OrderResponse } from '@/models/orders'
import { Eye, Printer, SquarePen } from 'lucide-react'
import React from 'react'
import { useQuery } from '@tanstack/react-query'
import axios from 'axios'
import DetailsOrder from './DetailsOrder'
import Update from './Update'

// ================== CONSTANT ==================
export const convertStatus: Record<number, string> = {
  0: 'Đang xử lý',
  1: 'Đã xác nhận',
  2: 'Đang vận chuyển',
  3: 'Đã giao xong',
  4: 'Đã hủy'
}

// ================== TYPES ==================
type OrderViewModel = {
  orderCode: string
  userId: string
  itemCount: number
  finalAmount: string
  status: string
  createdAt: string
  completedAt: string
  raw: OrderResponse // giữ lại data gốc để dùng cho action
}

// ================== HELPERS ==================
const formatCurrency = (value: number) =>
  value.toLocaleString('vi-VN', {
    style: 'currency',
    currency: 'VND'
  })

const formatDate = (date: string | null) => {
  if (!date) return 'Chưa hoàn thành'
  return new Date(date).toLocaleDateString('vi-VN')
}

// ================== API ==================
const getOrders = async (): Promise<OrderResponse[]> => {
  const res = await axios.get(API_ENDPOINTS.ORDER.GETALL)
  return res.data
}

// ================== COMPONENT ==================
function Order() {
  const result = useQuery({
    queryKey: ['orders'],
    queryFn: getOrders,
    select: (data): OrderViewModel[] =>
      data.map((item) => ({
        orderCode: `${item.id}-${item.orderCode}`,
        userId: item.userId?.slice(-12) ?? '',
        itemCount: item.items.length,
        finalAmount: formatCurrency(item.finalAmount),
        status: convertStatus[item.status],
        createdAt: formatDate(item.createdAt),
        completedAt: formatDate(item.completedAt),
        raw: item
      }))
  })

  // ================== HANDLE STATE ==================
  if (result.isLoading) {
    return <div className="p-4">Đang tải dữ liệu...</div>
  }

  if (result.isError) {
    return <div className="p-4 text-red-500">Lỗi khi tải dữ liệu</div>
  }

  // ================== TABLE CONFIG ==================
  const headerTable: ColumnConfig<OrderViewModel>[] = [
    { header: 'Mã đơn', key: 'orderCode' },
    { header: 'Mã người dùng', key: 'userId' },
    { header: 'Số sản phẩm', key: 'itemCount' },
    { header: 'Số tiền thanh toán', key: 'finalAmount' },
    { header: 'Trạng thái', key: 'status' },
    { header: 'Ngày tạo', key: 'createdAt' },
    { header: 'Ngày hoàn thành', key: 'completedAt' },
    {
      header: 'Thao tác',
      key: 'action',
      render: (item) => (
        <div className="flex flex-row gap-2">
          <DetailsOrder item={item.raw}>
            <Eye />
          </DetailsOrder>
          <Update item={item.raw}>
            <SquarePen />
          </Update>
        </div>
      )
    }
  ]

  // ================== RENDER ==================
  return (
    <div className="w-full">
      <div className="h-20 flex items-center justify-between">
        <h3 className="text-header">Danh sách đơn hàng</h3>

        <Button variant="outline">
          <Printer />
          In báo cáo
        </Button>
      </div>

      <TableCustom
        columns={headerTable}
        data={result.data ?? []}
        caption="Danh sách đơn hàng"
      />
    </div>
  )
}

export default Order