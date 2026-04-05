import Create from './Create'
import { type categories } from './DataTable'
import { API_ENDPOINTS } from '@/constants/urls'
import Delete from './Delete'
import {
  Table,
  TableRow,
  TableHeader,
  TableHead,
  TableBody,
  TableCell
} from '@/components/ui/table'
import { SquarePen } from 'lucide-react'
import type { BaseResponse } from '@/models/products'
import { useQuery, useQueryClient } from '@tanstack/react-query'
import axios from 'axios'

export default function Category() {
  const queryClient = useQueryClient()
  const url = API_ENDPOINTS.CATEGORY.GETALL

  // ================= FETCH =================
  const { data, isLoading, isRefetching } = useQuery({
    queryKey: ['categories'],
    queryFn: async (): Promise<BaseResponse<categories>> => {
      const response = await axios.get(url)
      return response.data
    }
  })

  // ================= TRANSFORM =================
  const processData =
    data?.data?.map((u) => ({
      ...u,
      image:
        !u.images || u.images === ''
          ? 'Chưa có hình ảnh'
          : u.images
    })) ?? []

  // ================= REFRESH =================
  const refresh = () => {
    queryClient.invalidateQueries({ queryKey: ['categories'] })
  }

  return (
    <div className="w-full">
      <div className="min-h-20 flex justify-between items-center">
        <h3 className="text-header">Danh mục sản phẩm</h3>

        <Create refresh={refresh} />
      </div>

      <Table>
        <TableHeader>
          <TableRow>
            <TableHead>Mã Danh mục</TableHead>
            <TableHead>Tên Danh mục</TableHead>
            <TableHead>Hình ảnh</TableHead>
            <TableHead>Slug</TableHead>
            <TableHead>Action</TableHead>
          </TableRow>
        </TableHeader>

        <TableBody>
          {isLoading ? (
            <TableRow>
              <TableCell colSpan={5} className="text-center h-24">
                Đang tải...
              </TableCell>
            </TableRow>
          ) : processData.length > 0 ? (
            processData.map((item) => (
              <TableRow key={item.id}>
                <TableCell>{item.id}</TableCell>
                <TableCell>{item.name}</TableCell>
                <TableCell>{item.image}</TableCell>
                <TableCell>{item.slug}</TableCell>
                <TableCell>
                  <Delete refresh={refresh} item={item}>
                    <SquarePen className="cursor-pointer" />
                  </Delete>
                </TableCell>
              </TableRow>
            ))
          ) : (
            <TableRow>
              <TableCell colSpan={5} className="h-24 text-center">
                Không có dữ liệu
              </TableCell>
            </TableRow>
          )}
        </TableBody>
      </Table>

      {/* 🔥 refetch indicator */}
      {isRefetching && (
        <div className="text-sm text-gray-500 mt-2">
          Đang cập nhật dữ liệu...
        </div>
      )}
    </div>
  )
}