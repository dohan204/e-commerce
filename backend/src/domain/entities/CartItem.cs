using domain.exceptions;

namespace domain.entities
{
    public class CartItem : BaseEntity
    {
        public int ProductId {get; private set;}
        public int Quantity {get; private set;}
        public decimal UnitPrice {get; private set;}

        private CartItem() {}
        public CartItem(int productId, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
        public static CartItem Created(int productId, int quantity, decimal price)
        {
            
            return new CartItem(productId, quantity, price);
        }
        public void Increment(int quantity)
        {
            if(quantity < 0) 
               throw new DomainException("Số lượng tăng thêm phải lớn hơn 0");
            Quantity += quantity;
        }

        public void UpdateQuantitty(int quantity)
        {
            if(quantity < 0)
                throw new DomainException("Số lượng cập nhật phải lớn hơn 0");
            Quantity = quantity;
        }
        public void validateInput(int productId, int quantity)
        {
            if(productId < 0) 
                throw new DomainException("ProductId Invalid");
            if(quantity < 0)
            {
                throw new DomainException("Số lượng phải lớn hơn 0");
            }
        }
    }
}