
import useFetch from '@/hooks/use-fetchs'
import { Search, SquarePen, Trash } from 'lucide-react'
import Create from './Create'
import Update from './Update'
import Delete from './Delete'
import { Skeleton } from '@/components/ui/skeleton'
import type { BaseResponse, PagedResult, Product } from '@/models/products'
import { useEffect, useState } from 'react'
import { API_ENDPOINTS } from '@/constants/urls'
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select'
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from '@/components/ui/table'
import type { categories } from '../categories/DataTable'
import useFetchs from '@/hooks/use-fetch'
import useFetchSingle from '@/hooks/use-fetchs'
import {
  Pagination,
  PaginationContent,
  PaginationItem,
  PaginationLink,
  PaginationNext,
  PaginationPrevious,
} from "@/components/ui/pagination";
const Products = () => {
  const [search, setSearch] = useState<string>('');
  const [page, setPage] = useState<number>(1);
  const [debound, setDebound] = useState<string>('');
  const [pageSize, setPageSize] = useState<number>(10);
  const [categoryId, setCategoryId] = useState<number>();

  useEffect(() => {
    const handler = setTimeout(() => {
      setDebound(search)
      setPage(1);
    }, 500)
    return () => clearTimeout(handler);
  }, [search])
  const { data, loading, refresh } =
    useFetchSingle<PagedResult<Product>>(API_ENDPOINTS.PRODUCT.PAGINATION(page, pageSize, debound, categoryId));
  const { data: categories } = useFetchSingle<BaseResponse<categories>>(API_ENDPOINTS.CATEGORY.GETALL);

  const dataRender = data?.items.map((item) => ({
    ...item,
    stock: (item.stock === null) ? 'Chưa có thống kê' : item.stock,
    imageUrl: (item.imageUrl === null) ? "Chưa thiết lập hình ảnh" : item.imageUrl,
  }))

  const totalPages = Math.ceil((data?.total || 0) / pageSize);

  const visiblePages = Array.from({ length: totalPages }, (_, i) => i + 1)
    .filter(p => p >= page - 2 && p <= page + 2);
  return (
    <div className='w-full'>
      <div className='h-20 flex justify-around items-center'>
        <div className='float-start'>
          <h3 className='text-header font-sans'> Danh sách sản phẩm</h3>
        </div>
        <div className='flex gap-8'>
          <div>
            {/* lọc sản phẩm */}
            <Select onValueChange={(value) => {
              setCategoryId(value === "all" ? undefined : Number(value))
              setPage(1);
            }}>
              <SelectTrigger className="w-[180px]">
                <SelectValue placeholder="Chọn danh mục" />
              </SelectTrigger>
              <SelectContent>
                <SelectItem value="all">Tất cả</SelectItem>
                {categories ? categories?.data.map((cate) => (
                  <SelectItem key={cate.id} value={cate.id.toString()}>
                    {cate.name}
                  </SelectItem>
                )) : <SelectItem value="all">Không có danh mục nào</SelectItem>}
              </SelectContent>
            </Select>
          </div>
          {/* tìm kiếm */}
          <div className='ml-0.5 relative'>
            <input
              type='text'
              placeholder='tìm kiếm sản phẩm'
              value={search}
              onChange={e => setSearch(e.target.value)}
              className='pl-3 pr-3 p-1.5 ml-2 border-2 rounded-xl '
            />
            <Search className='absolute top-2 right-3 font-black' />
          </div>
        </div>
        <div className='float-end'>
          <Create categories={categories?.data} refresh={refresh} />
        </div>
      </div>
      <div>
        <Table>
          <TableHeader>
            <TableRow>
              <TableHead>Mã sản phẩm</TableHead>
              <TableHead>Tên sản phẩm</TableHead>
              <TableHead>Số lượng tồn</TableHead>
              <TableHead>Giá bán</TableHead>
              <TableHead>Hình ảnh</TableHead>
              <TableHead>Action</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            {loading
              ? Array.from({ length: pageSize }).map((_, i) => (
                <TableRow key={i}>
                  {Array.from({ length: 6 }).map((_, j) => (
                    <TableCell key={j}>
                      <Skeleton className="h-4 w-full bg-gray-200" />
                    </TableCell>
                  ))}
                </TableRow>
              ))
              : dataRender!.length > 0 ? dataRender?.map((item) => (
                <TableRow key={item.id}>
                  <TableCell>{item.id}</TableCell>
                  <TableCell>{item.name}</TableCell>
                  <TableCell>{item.stock}</TableCell>
                  <TableCell>{item.price.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })}</TableCell>
                  <TableCell>{item.imageUrl}</TableCell>
                  <TableCell className='flex items-center'>
                    <Update refresh={refresh} item={item} categories={categories?.data}>
                      <SquarePen className='cursor-pointer' />
                    </Update>
                    <Delete refresh={refresh} item={item}>
                      <Trash className='cursor-pointer' />
                    </Delete>
                  </TableCell>
                </TableRow>
              )) : <TableRow>
                <TableCell colSpan={6} className="h-24 text-center">
                  Không có dữ liệu
                </TableCell>
              </TableRow>
            }
          </TableBody>
        </Table>
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
                onClick={() => setPage(p => Math.min(data?.total || 1, p + 1))}
              />
            </PaginationItem>

          </PaginationContent>
        </Pagination>
      </div>
    </div>
  )
}

export default Products