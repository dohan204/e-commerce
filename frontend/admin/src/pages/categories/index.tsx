import TableCustom, { type ColumnConfig } from '@/components/TableData'

import Create from './Create'
import { type categories } from './DataTable';
import { API_ENDPOINTS } from '@/constants/urls';
import Delete from './Delete';
import { Table, TableRow, TableHeader, TableHead, TableBody, TableCell } from '@/components/ui/table';
import { SquarePen } from 'lucide-react';
import useFetchSingle from '@/hooks/use-fetchs';
import type { BaseResponse } from '@/models/products';
export default function Category() {

    const url = API_ENDPOINTS.CATEGORY.GETALL
    const { data, refresh } = useFetchSingle<BaseResponse<categories>>(url);
    console.log('data: ', data)
    const processData = data ? data?.data.map(u => ({
        ...u,
        image: (u.images === null || u.images === '') ? "Chưa có hình ảnh" : u.images
    })) : [];
    console.log('processData: ', processData)
    return (
        <div className='w-full'>
            <div className='min-h-20'>
                {/* button */}
                <div className='float-end'>
                    <Create refresh={refresh} />
                </div>
                <div className='float-start'>
                    <h3 className='text-header'>Danh mục sản phẩm</h3>
                </div>
            </div>
            <Table>
                <TableHeader>
                    <TableRow>
                        <TableHead>Mã Danh mục</TableHead>
                        <TableHead>Tên Danh mục</TableHead>
                        <TableHead>Hình ảnh</TableHead>
                        <TableHead>Ngày tạo</TableHead>
                        <TableHead>Action</TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>
                    { data?.data ? data?.data?.map((item) => (
                        <TableRow key={item.id}>
                            <TableCell>{item.id}</TableCell>
                            <TableCell>{item.name}</TableCell>
                            <TableCell>{item.images}</TableCell>
                            <TableCell>{item.slug}</TableCell>
                            <TableCell>
                                <Delete refresh={refresh} item={item}>
                                    <SquarePen />
                                </Delete>
                            </TableCell>
                        </TableRow>
                    )) : <TableRow>
                        <TableCell colSpan={5} className="h-24 text-center">
                            Không có dữ liệu
                        </TableCell>
                    </TableRow>}
                </TableBody>
            </Table>
        </div>
    )
}
