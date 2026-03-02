using domain.exceptions;

namespace domain.entities
{
    public enum StatusOrder { pending, confirmed, shipping, delivered, cancelled }
    public enum PaymentMethod { cod, vnpay, momo, card }
    public enum PaymentStatus { unpaid, paid, refunded, failed }

    public class Order : BaseEntity
    {
        public string OrderCode { get; private set; } = string.Empty;
        public Guid UserId { get; private set; }

        public decimal TotalAmount { get; private set; }
        public decimal DiscountAmount { get; private set; }
        public decimal ShippingFee { get; private set; }
        public decimal FinalAmount { get; private set; }

        public StatusOrder Status { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }
        public PaymentStatus PaymentStatus { get; private set; }

        public string ShippingAddress { get; private set; } = string.Empty;
        public int? VoucherId { get; private set; }
        public string? Note { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }

        protected Order() { }

        private Order(
            Guid userId,
            decimal totalAmount,
            decimal discountAmount,
            decimal shippingFee,
            PaymentMethod paymentMethod,
            string shippingAddress,
            int? voucherId,
            string? note
        )
        {
            if (userId == Guid.Empty)
                throw new DomainException("User invalid");

            if (totalAmount <= 0)
                throw new DomainException("Total amount invalid");

            UserId = userId;
            TotalAmount = totalAmount;
            DiscountAmount = discountAmount;
            ShippingFee = shippingFee;
            FinalAmount = totalAmount - discountAmount + shippingFee;

            PaymentMethod = paymentMethod;
            PaymentStatus = PaymentStatus.unpaid;
            Status = StatusOrder.pending;

            ShippingAddress = shippingAddress;
            VoucherId = voucherId;
            Note = note;

            CreatedAt = DateTime.UtcNow;
        }

        public static Order Create(
            Guid userId,
            decimal totalAmount,
            decimal discountAmount,
            decimal shippingFee,
            PaymentMethod paymentMethod,
            string shippingAddress,
            int? voucherId,
            string? note
        )
        {
            return new Order(
                userId,
                totalAmount,
                discountAmount,
                shippingFee,
                paymentMethod,
                shippingAddress,
                voucherId,
                note
            );
        }

        public void MarkPaymentSuccess()
        {
            PaymentStatus = PaymentStatus.paid;
            Status = StatusOrder.confirmed;
        }

        public void MarkPaymentFailed()
        {
            PaymentStatus = PaymentStatus.failed;
        }

        public void StartShipping()
        {
            if (Status != StatusOrder.confirmed)
                throw new DomainException("Order not confirmed");

            Status = StatusOrder.shipping;
        }

        public void Complete()
        {
            Status = StatusOrder.delivered;
            CompletedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            if (Status == StatusOrder.delivered)
                throw new DomainException("Cannot cancel delivered order");

            Status = StatusOrder.cancelled;
        }
    }
}