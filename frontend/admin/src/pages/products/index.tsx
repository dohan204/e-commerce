import Icons from '@/components/icons'
import type { ColumnConfig } from '@/components/TableData'
import TableCustom from '@/components/TableData'
import { Button } from '@/components/ui/button'
import useFetch from '@/hooks/use-fetchs'
import { Eye, Icon, SquarePen, Trash } from 'lucide-react'
import React from 'react'
import Create from './Create'
import useFetchs from '@/hooks/use-fetch'
interface res {
  message: string,
  data: response[]
}
interface response {
  id: number,
  name: string,
  description: string | null,
  stock: number,
  price: number,
  salePrice: number | null,
  imageUrl: string | null,
  avgRating: number,
  action: any
}

const HeaderTable: ColumnConfig<any>[] = [
  {header: 'Mã sản phẩm', key: 'id'},
  {header: 'Tên sản phẩm', key: 'name'},
  {header: 'Mô tả', key: 'description'},
  {header: 'Số lượng tồn', key: 'stock'},
  {header: 'Giá bán', key: 'price'},
  {header: 'Giảm giá', key: 'salePrice'},
  {header: 'Hình ảnh', key: 'imageUrl'},
  {header: 'Đánh giá', key: 'avgRating'},
  {header: 'Action', key: 'action', render: (item) => <div className='flex items-center gap-2'>
    <SquarePen className='cursor-pointer' />
    <Trash className='cursor-pointer'/>
  </div>}
]

const Product = () => {
  const {data, loading, error, refresh} = useFetch<res>('http://localhost:5255/api/product');
  
  if(!data) return;
  const dataRender = data!.data.map((item) => ({
    ...item,
    stock: (item.stock === null) ? 'Chưa có thống kê' : item.stock,
    imageUrl: (item.imageUrl === null) ? "Chưa thiết lập hình ảnh" : item.imageUrl,
  }))
  return (
    <div className='w-full'>
      <div className='h-20 items-center'>
        <div className='float-start'>
          <h3 className='text-header font-sans'> Danh sách sản phẩm</h3>
        </div>
        <div className='float-end'>
          <Create refresh={refresh} />
        </div>
      </div>
      <TableCustom columns={HeaderTable} data={dataRender} caption='Danh sách sản phẩm' />
    </div>
  )
}

export default Product