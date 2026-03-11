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
        public ICollection<OrderItem> Items {get; set;}

        private Order() { }

        public Order(
            Guid userId,
            string shippingAddress,
            int? voucherId,
            string? note
        )
        {
            if (userId == Guid.Empty)
                throw new DomainException("User invalid");
            OrderCode = Guid.NewGuid().ToString().Substring(0, 10);
            UserId = userId;
            PaymentMethod = PaymentMethod.cod;
            PaymentStatus = PaymentStatus.unpaid;
            Status = StatusOrder.pending;
            ShippingAddress = shippingAddress;
            VoucherId = voucherId;
            Note = note;
            Items = new List<OrderItem>();
            CreatedAt = DateTime.UtcNow;
        }

        public static Order Create(
            Guid userId,
            string shippingAddress,
            int? voucherId,
            string? note
        )
        {
            return new Order(
                userId,
                shippingAddress,
                voucherId,
                note
            );
        }
        public void Update(decimal discountAmount, decimal shippingFee, string shippingAddress, string? note)
        {
            DiscountAmount = discountAmount;
            ShippingFee = shippingFee;
            ShippingAddress = shippingAddress;
            Note = note;
        }
        public void UpdateOrderItem(int productId, int quantity, decimal price)
        {
            var item = OrderItem.Update(productId, quantity, price);
            Items.Add(item);
            RecalculateAmount();
        }
        public void AddOrderItem(int productId, int quantity, decimal price)
        {
            var item = OrderItem.Create(this.Id, productId, quantity, price);
            Items.Add(item);
            RecalculateAmount();
        }
        
        public void SetAddressToOrder(string address)
        {
            ShippingAddress = address;
        }
        public void RecalculateAmount()
        {
            TotalAmount = Items.Sum(x => x.Quantity * x.Price);
            FinalAmount = TotalAmount - DiscountAmount + ShippingFee;
        }
        public void SetAmount(decimal total, decimal final)
        {
            TotalAmount = total;
            FinalAmount = final;
        }
        public void ApplyDiscount(decimal discountAmount)
        {
            if(discountAmount < 0)
                throw new DomainException("Disscount Invalid");
            
            DiscountAmount = discountAmount;
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
        public void ReturnFinal(decimal totalAmount)
        {
            FinalAmount = totalAmount;
        }
    }
}