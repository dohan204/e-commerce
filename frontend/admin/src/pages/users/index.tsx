import React from 'react'
import Create from './Create'
import useFetchs from '@/hooks/use-fetch'
import { type UserResponse } from '@/models/users'
import { API_ENDPOINTS } from '@/constants/urls'
import TableCustom from '@/components/TableData'
import Update from './Update'
import { SquarePen, Trash } from 'lucide-react'
import Delete from './Delete'

const User = () => {

  const url = API_ENDPOINTS.USER.GETALL
  const { data, refresh } = useFetchs<UserResponse>(url);
  const dataConvert = data.map(item => ({
    ...item,
    createdAt: new Date(item.createdAt).toLocaleDateString('vi-VN'),
    updatedAt: new Date(item.updatedAt).toLocaleDateString('vi-VN'),
  }));

  const HeaderTable = [
  { header: 'Mã người dùng', key: 'id', },
  { header: 'Tên đăng nhập', key: 'name', },
  { header: 'Họ tên', key: 'fullName', },
  { header: 'email', key: 'email', },
  { header: 'Ngày tạo', key: 'createdAt', },
  { header: 'Ngày sửa', key: 'updatedAt', },
  { header: 'Action', key: 'action', render: (item: any) => (
    <div className='flex items-center gap-4'>
      <Update item={item}>
        <SquarePen />
      </Update>
      <Delete refresh={refresh} item={item}>
        <Trash />
      </Delete>
    </div>
  )}
]
  console.log(data)
  return (
    <div className='w-full'>
      <div className='h-20'>
        <div className='float-start'>
          <h3 className='text-header'>Danh sách người dùng</h3>
        </div>
        <div className='float-end'>
          <Create refresh={refresh} />
        </div>
      </div>
      <TableCustom columns={HeaderTable} data={dataConvert} caption='Danh sách người dùng' />
    </div>
  )
}

export default User