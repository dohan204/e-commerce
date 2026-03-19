import Icons from '@/components/icons'
import type { ColumnConfig } from '@/components/TableData'
import TableCustom from '@/components/TableData'
import { Button } from '@/components/ui/button'
import useFetch from '@/hooks/use-fetchs'
import { Eye, Icon, SquarePen, Trash } from 'lucide-react'
import React from 'react'
import Create from './Create'
import useFetchs from '@/hooks/use-fetch'
import Update from './Update'
import Delete from './Delete'
import { Skeleton } from '@/components/ui/skeleton'
import type { BaseResponse, Product } from '@/models/products'
const Products = () => {
  const { data, loading, refresh } = useFetch<BaseResponse<Product>>('http://localhost:5255/api/product');
  const HeaderTable: ColumnConfig<any>[] = [
    { header: 'Mã sản phẩm', key: 'id' },
    { header: 'Tên sản phẩm', key: 'name' },
    { header: 'Số lượng tồn', key: 'stock' },
    { header: 'Giá bán', key: 'price' },
    { header: 'Giảm giá', key: 'salePrice' },
    { header: 'Hình ảnh', key: 'imageUrl' },
    {
      header: 'Action', key: 'action', render: (item) => <div className='flex items-center gap-2 flex-row'>
        <Update item={item}><SquarePen className='cursor-pointer' /></Update>
        <Delete refresh={refresh} item={item}><Trash className='cursor-pointer' /></Delete>
      </div>
    }
  ]
    const LoadData = () =>
     (
      Array.from({ length: 10 }).map((_, colIndex) => (
        <div key={colIndex} className="flex flex-row gap-2 mb-2">
          {Array.from({ length: 9 }).map((_, rowIndex) => (
            <Skeleton key={rowIndex} className="w-20 h-4 bg-gray-200 flex-1" />
          ))}
        </div>
      ))
    )
  if(!data)
    return 0;
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
      {loading ? <LoadData /> : data ? <TableCustom columns={HeaderTable} data={dataRender} caption='Danh sách sản phẩm' /> : []} 
    </div>
  )
}

export default Products