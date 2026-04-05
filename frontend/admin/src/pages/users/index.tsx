import Create from './Create'
import { type User, type UserResponse } from '@/models/users'
import { API_ENDPOINTS } from '@/constants/urls'
import Update from './Update'
import { SquarePen, Trash } from 'lucide-react'
import Delete from './Delete'
import { useQuery } from '@tanstack/react-query'
import type { BaseResponse } from '@/models/products'
import axios from 'axios'
import {Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from '@/components/ui/table'
import { Skeleton } from '@/components/ui/skeleton'

const User = () => {
  const { data, isLoading } = useQuery({
    queryKey: ['users'],
    queryFn: async (): Promise<BaseResponse<UserResponse>> => {
      const response = await axios.get<BaseResponse<UserResponse>>(API_ENDPOINTS.USER.GETALL);
      if (!response.data)
        throw new Error("lỗi gọi dữ liệu");
      return response.data;
    }
  })

  console.log('data', data);
  const dataConvert = data?.data.map(item => ({
    ...item,
    createdAt: new Date(item.createdAt).toLocaleDateString('vi-VN'),
    updatedAt: new Date(item.updatedAt).toLocaleDateString('vi-VN'),
  }));
  console.log(data)
  return (
    <div className='w-full'>
      <div className='h-20'>
        <div className='float-start'>
          <h3 className='text-header'>Danh sách người dùng</h3>
        </div>
        <div className='float-end'>
          <Create />
        </div>
      </div>
      <Table>
        <TableHeader>
          <TableRow>
            <TableHead>Mã Người dùng</TableHead>
            <TableHead>Tên đăng nhập</TableHead>
            <TableHead>Họ tên</TableHead>
            <TableHead>Email</TableHead>
            <TableHead>Ngày tạo</TableHead>
            <TableHead>Ngày sửa</TableHead>
            <TableHead>Thao tác</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {isLoading ? (
            Array.from({ length: 5 }).map((_, i) => (
              <TableRow key={i}>
                {/* Tạo số lượng Cell tương ứng với số cột của bạn (7 cột) */}
                {Array.from({ length: 7 }).map((_, j) => (
                  <TableCell key={j}>
                    <Skeleton className="h-6 w-full" />
                  </TableCell>
                ))}
              </TableRow>
            ))
          ) : dataConvert?.map((item, i) => (
            <TableRow key={item.userId}>
              <TableCell>{item.userId}</TableCell>
              <TableCell>{item.name}</TableCell>
              <TableCell>{item.fullName}</TableCell>
              <TableCell>{item.email}</TableCell>
              <TableCell>{item.createdAt}</TableCell>
              <TableCell>{item.updatedAt}</TableCell>
              <TableCell>
                <div className='flex items-center gap-4'>
                  <Update item={item}>
                    <SquarePen />
                  </Update>
                  <Delete item={item}>
                    <Trash />
                  </Delete>
                </div>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </div>
  )
}

export default User