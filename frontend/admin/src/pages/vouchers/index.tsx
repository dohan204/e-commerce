import type { ColumnConfig } from '@/components/TableData'
import TableCustom from '@/components/TableData'
import { API_ENDPOINTS } from '@/constants/urls'
import type { VoucherResponse, Vouchers } from '@/models/vouchers'
import { Create } from './Create'
import { Plus, Trash } from 'lucide-react'
import Delete from './Delete'
import { useQuery } from '@tanstack/react-query'
import axios from 'axios'

const Voucher = () => {

  const {data, isRefetching} = useQuery({
    queryKey: ['vouchers'],
    queryFn: async (): Promise<Vouchers> => {
      const response = await axios.get<Vouchers>(API_ENDPOINTS.VOUCHER.GETALL);
      return response.data;
    }
  })
  if(data === null || data === undefined) 
    return;

  const convert = data.data.map(item => ({
    ...item,
    expiryDate: new Date(item.expiryDate).toLocaleDateString('vi-VN')
  }));


  const tableHeader: ColumnConfig<any>[] = [
    {header: 'Số thứ tự', key: 'id'},
    {header: 'Mã', key: 'code'},
    {header: 'Kiểu giảm giá', key: 'discountType'},
    {header: 'Giá trị', key: 'value'},
    {header: 'Đơn tối thiểu', key: 'minOrder'},
    {header: 'Số lần sử dụng', key: 'maxUsage'},
    {header: 'Hạn sử dụng', key: 'expiryDate'},
    {header: 'Thao tác', key: 'action', render: (item) => <div>
      <Delete item={item}>
        <Trash />
      </Delete>
    </div>},
  ]
  return (
    <div className='w-full'>
      <div className='h-20'>
        <div className='float-start'>
          <h3 className='text-header'>Danh sách Mã giảm giá</h3>
        </div>
        <div className='float-end'>
          <Create>
            <Plus />
            Tạo mới
          </Create>
        </div>
      </div>
      <div>
        <TableCustom columns={tableHeader} data={convert} caption='danh sách mã giảm giá' />
      </div>
    </div>
  )
}

export default Voucher