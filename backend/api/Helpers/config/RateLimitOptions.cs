namespace api.Helpers.Dtos.config
{
    public class RateLimitOptions
    {
        public static string RateLimit { get; set; } = "RateLimit";
        public int PermitLimit {get; set;} // số request tối đa
        public int Window {get; set;} // thời gian sử dụng request 
        public int SegmentsPerWindow {get; set;} // chia nhỏ ra
        public int QueueLimit {get; set;} // hàng đợi
    }
}