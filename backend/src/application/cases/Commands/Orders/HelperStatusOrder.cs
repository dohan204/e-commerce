namespace application.cases.Commands.Orders
{
    public static class HelperStatusOrder
    {
        public static string GetStringStatus (int status)
        {
            return status switch
            {
                0 => $"Đơn hàng đang được xử lý",
                1 => $"Đơn hàng đã được xác nhận",
                2 => $"Đơn hàng đang được vận chuyển",
                3 => $"Đơn hàng giao thành công.",
                4 => $"Đơn hàng đã bị hủy"
            };
        } 
    }
}