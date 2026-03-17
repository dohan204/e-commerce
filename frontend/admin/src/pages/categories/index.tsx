import TableCustom, { type ColumnConfig } from '@/components/TableData'

import Create from './Create'
import useFetch from '@/hooks/use-fetch'
import { type response, HeaderName } from './DataTable';
export default function Category() {
    const { data, loading, error, refresh } = useFetch<response>('http://localhost:5255/api/category/alls');
    const processData = data.map(u => ({
        ...u,
        image: (u.image === null || u.image === '') ? "Chưa có hình ảnh" : u.image
    }))
    return (
        <div className='w-full'>
            <div className='min-h-20 shadow-2xl rounded-2xl'>
                {/* button */}
                <div className='float-end p-4'>
                    <Create refresh={refresh} />
                </div>
                <div className='float-start p-4'>
                    <h3 className='text-header'>Danh mục sản phẩm</h3>
                </div>
            </div>
            <TableCustom columns={HeaderName} data={processData} caption='Danh sách các danh mục sản phẩm.' />

        </div>
    )
}
