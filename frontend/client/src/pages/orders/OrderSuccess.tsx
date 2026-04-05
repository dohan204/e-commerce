import { useNavigate } from "react-router";

const OrderSuccess = () => {
    const navigate = useNavigate();
  return (
    <div className="flex flex-col items-center justify-center min-h-[70vh] gap-6">
      <div className="text-green-500 text-6xl">✔</div>
      <h2 className="text-2xl font-bold">
        Đặt hàng thành công!
      </h2>
      <div className="text-center space-y-2">
        <p>Mã đơn: <span className="font-semibold">#123456</span></p>
        <p>Ngày: 05/04/2026</p>
        <p>Tổng tiền: <span className="text-red-500">1.250.000đ</span></p>
      </div>
      <div className="flex gap-4 mt-4">
        <button className="px-4 py-2 border rounded"
            onClick={() => navigate('/orders')}
        >
          Xem đơn hàng
        </button>
        <button className="px-4 py-2 bg-black text-white rounded"
            onClick={() => navigate('/categories')}
        >
          Tiếp tục mua sắm
        </button>
      </div>

    </div>
  )
}

export default OrderSuccess;