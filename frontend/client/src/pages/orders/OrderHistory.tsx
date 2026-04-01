import { Button } from '@/components/ui/button';
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from '@/components/ui/table'
import { API_ENDPOINTS } from '@/constants/UrlGlobal';
import useFetch from '@/hooks/useFetch'
import { useUserContext } from '@/hooks/useUserContext';
import type { Order } from '@/models/Orders';
import type { Base } from '@/models/response/base';
import axios from 'axios';
import { useState } from 'react';
import { toast } from 'sonner';

const OrderStatus = {
  0: "Đăng xử lý",
  1: "Đã xác nhận",
  2: "Đang giao hàng",
  3: "Đã giao hàng",
  4: "Đã hủy"
}
const OrderHistory = () => {
  const { user } = useUserContext();
  const userId = user?.id || '';
  const [count, setCount] = useState(0);
  const { data } = useFetch<Base<Order>>(API_ENDPOINTS.ORDER.GETBYUSERID(userId), [count]);


  const token = localStorage.getItem('token');
  if (!token)
    return;
  const HandleCancelOrder = async (id: number) => {
    try {
      const res = await fetch(API_ENDPOINTS.ORDER.CANCELLED(id), {
        method: 'PUT',
        headers: {
          'Authorization': `Bearer ${token}`
        }
      });

      if (!res.ok) {
        throw new Error("Failed.");
      };

      toast.success("Hủy Đơn hàng thành công.", { position: 'top-center' });
      setCount(prev => prev + 1);
    } catch (err) {
      toast.error("Hủy đơn hàng thất bại.", { position: 'top-center' })
      console.log(err);
    }
  }



  const HandleDelete = async (id: number) => {
    try {
      const res = await fetch(API_ENDPOINTS.ORDER.DELETE(id), {
        method: 'DELETE',
        headers: {
          'Authorization': `Bearer ${token}`
        },
      })
      if (!res.ok) {
        throw new Error("Failed.");
      };

      toast.success("Xóa Đơn hàng thành công.", { position: 'top-center' });
      setCount(prev => prev - 1);
    } catch (err) {
      toast.error("Xóa đơn hàng thất bại.", { position: 'top-center' })
      console.log(err);
    }
  }
  return (
    <div className='px-4 py-6'>
      <h1 className='text-xl font-heading font-stretch-100%'>Lịch sử đơn hàng</h1>
      <p>Đây là trang lịch sử đơn hàng của bạn. Tại đây bạn có thể xem lại tất cả các đơn hàng đã đặt trước đó, bao gồm chi tiết về sản phẩm, ngày đặt hàng, và trạng thái đơn hàng.</p>
      <p>Chúng tôi hy vọng bạn sẽ hài lòng với trải nghiệm mua sắm tại Shop byHan. Nếu bạn có bất kỳ câu hỏi nào về đơn hàng của mình, đừng ngần ngại liên hệ với chúng tôi qua trang liên hệ hoặc gửi email đến support@shopbyhan.com</p>
      <p>Cảm ơn bạn đã tin tưởng và lựa chọn Shop byHan cho nhu cầu mua sắm của mình!</p>
      <div className='mt-6'>
        <Table>
          <TableHeader>
            <TableRow>
              <TableHead>Ngày đặt hàng</TableHead>
              <TableHead>Trạng thái</TableHead>
              <TableHead>Số lượng</TableHead>
              <TableHead>Tổng tiền</TableHead>
              <TableHead className='flex justify-center items-center'>Thao tác</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            {data?.data?.map((order) => (
              <TableRow key={order.id}>
                <TableCell>{new Date(order.createdAt).toLocaleDateString('vi-VN')}</TableCell>
                <TableCell>{OrderStatus[order.status as keyof typeof OrderStatus]}</TableCell>
                <TableCell>{order.items.length}</TableCell>
                <TableCell>{order.totalAmount.toFixed(2)}</TableCell>
                <TableCell className='flex items-center justify-center gap-2'>
                  {order.status === 4 || order.status === 3 ? (
                    <div>
                      <Button variant={'outline'} className='cursor-pointer'>Mua lại</Button>
                      <Button variant={'secondary'} className='cursor-pointer' onClick={() => HandleDelete(order.id)}>Xóa lịch sử</Button>
                    </div>
                  ) : ''}
                  {
                    order.status === 2 ? (
                      <Button variant={'ghost'}>Đơn hàng sẽ nhanh chóng vận chuyển</Button>
                    ) : order.status !== 3 && order.status !== 4 ? (
                      <Button
                        variant={'destructive'}
                        onClick={() => HandleCancelOrder(order.id)}
                      >
                        Hủy đơn hàng
                      </Button>
                    ) : null
                  }
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </div>
    </div>
  )
}

export default OrderHistory