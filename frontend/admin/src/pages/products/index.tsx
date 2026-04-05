import { Search, SquarePen, Trash } from 'lucide-react'
import Create from './Create'
import Update from './Update'
import Delete from './Delete'
import { Skeleton } from '@/components/ui/skeleton'
import type { BaseResponse, PagedResult, Product } from '@/models/products'
import { useEffect, useState } from 'react'
import { API_ENDPOINTS } from '@/constants/urls'
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue
} from '@/components/ui/select'
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow
} from '@/components/ui/table'
import type { categories } from '../categories/DataTable'
import {
  Pagination,
  PaginationContent,
  PaginationItem,
  PaginationLink,
  PaginationNext,
  PaginationPrevious
} from "@/components/ui/pagination"
import { useQuery, useQueryClient } from '@tanstack/react-query'
import axios from 'axios'

const Products = () => {
  const queryClient = useQueryClient()

  const [search, setSearch] = useState('')
  const [debounce, setDebounce] = useState('')
  const [page, setPage] = useState(1)
  const [pageSize, setPageSize] = useState(10)
  const [categoryId, setCategoryId] = useState<number | undefined>()

  useEffect(() => {
    const handler = setTimeout(() => {
      setDebounce(search)
      setPage(1)
    }, 500)
    return () => clearTimeout(handler)
  }, [search])

  const { data, isLoading } = useQuery({
    queryKey: ['products', page, pageSize, debounce, categoryId],
    queryFn: async (): Promise<PagedResult<Product>> => {
      const res = await axios.get(
        API_ENDPOINTS.PRODUCT.PAGINATION(page, pageSize, debounce, categoryId)
      )
      return res.data
    }
  })

  const { data: categories} = useQuery({
    queryKey: ['categories'],
    queryFn: async (): Promise<BaseResponse<categories>> => {
      const response = await axios.get<BaseResponse<categories>>(API_ENDPOINTS.CATEGORY.GETALL);
      return response.data;
    }
  })

  const dataRender = data?.items?.map((item) => ({
    ...item,
    stock: item.stock ?? 'Chưa có thống kê',
    imageUrl: item.imageUrl ?? 'Chưa thiết lập hình ảnh'
  })) ?? []

  const totalPages = Math.ceil((data?.total || 0) / pageSize)

  const visiblePages = Array.from({ length: totalPages }, (_, i) => i + 1)
    .filter(p => p >= page - 2 && p <= page + 2)

  const refresh = () => {
    queryClient.invalidateQueries({ queryKey: ['products'] })
  }

  return (
    <div className='w-full'>
      <div className='h-20 flex justify-between items-center'>
        <h3 className='text-header font-sans'>Danh sách sản phẩm</h3>

        <div className='flex gap-6'>
          {/* FILTER */}
          <Select
            onValueChange={(value) => {
              setCategoryId(value === 'all' ? undefined : Number(value))
              setPage(1)
            }}
          >
            <SelectTrigger className="w-[180px]">
              <SelectValue placeholder="Chọn danh mục" />
            </SelectTrigger>
            <SelectContent>
              <SelectItem value="all">Tất cả</SelectItem>
              {categories?.data?.map((cate) => (
                <SelectItem key={cate.id} value={cate.id.toString()}>
                  {cate.name}
                </SelectItem>
              ))}
            </SelectContent>
          </Select>

          {/* SEARCH */}
          <div className='relative'>
            <input
              type='text'
              placeholder='Tìm kiếm sản phẩm'
              value={search}
              onChange={e => setSearch(e.target.value)}
              className='pl-3 pr-8 p-1.5 border rounded-xl'
            />
            <Search className='absolute top-2 right-2' />
          </div>
        </div>

        <Create categories={categories?.data} refresh={refresh} />
      </div>

      {/* TABLE */}
      <Table>
        <TableHeader>
          <TableRow>
            <TableHead>Mã</TableHead>
            <TableHead>Tên</TableHead>
            <TableHead>Tồn</TableHead>
            <TableHead>Giá</TableHead>
            <TableHead>Ảnh</TableHead>
            <TableHead>Action</TableHead>
          </TableRow>
        </TableHeader>

        <TableBody>
          {isLoading ? (
            Array.from({ length: pageSize }).map((_, i) => (
              <TableRow key={i}>
                {Array.from({ length: 6 }).map((_, j) => (
                  <TableCell key={j}>
                    <Skeleton className="h-4 w-full" />
                  </TableCell>
                ))}
              </TableRow>
            ))
          ) : dataRender.length > 0 ? (
            dataRender.map((item) => (
              <TableRow key={item.id}>
                <TableCell>{item.id}</TableCell>
                <TableCell>{item.name}</TableCell>
                <TableCell>{item.stock}</TableCell>
                <TableCell>
                  {item.price.toLocaleString('vi-VN', {
                    style: 'currency',
                    currency: 'VND'
                  })}
                </TableCell>
                <TableCell>{item.imageUrl}</TableCell>
                <TableCell className='flex gap-2'>
                  <Update refresh={refresh} item={item} categories={categories?.data}>
                    <SquarePen className='cursor-pointer' />
                  </Update>
                  <Delete refresh={refresh} item={item}>
                    <Trash className='cursor-pointer' />
                  </Delete>
                </TableCell>
              </TableRow>
            ))
          ) : (
            <TableRow>
              <TableCell colSpan={6} className="text-center h-24">
                Không có dữ liệu
              </TableCell>
            </TableRow>
          )}
        </TableBody>
      </Table>

      {/* PAGINATION */}
      <Pagination>
        <PaginationContent>

          <PaginationItem>
            <PaginationPrevious
              onClick={() => setPage(p => Math.max(1, p - 1))}
            />
          </PaginationItem>

          {visiblePages.map(p => (
            <PaginationItem key={p}>
              <PaginationLink
                isActive={page === p}
                onClick={() => setPage(p)}
              >
                {p}
              </PaginationLink>
            </PaginationItem>
          ))}

          <PaginationItem>
            <PaginationNext
              onClick={() => setPage(p => Math.min(totalPages, p + 1))}
            />
          </PaginationItem>

        </PaginationContent>
      </Pagination>
    </div>
  )
}

export default Products