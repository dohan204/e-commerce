// import React from 'react'
import { type ColumnConfig } from '@/components/TableData'

export const HeaderName: ColumnConfig<any>[] = [
  { header: 'Mã', key: "id", className: 'w-[100px] font-medium' },
  { header: 'Tên', key: 'name' },
  { header: 'Hình ảnh', key: 'image' },
  { header: 'Ngày tạo', key: 'slug' }
]

export type response = {
  id: number,
  name: string,
  image: string,
  slug: string
}
