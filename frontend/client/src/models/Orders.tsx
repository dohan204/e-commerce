export interface Order {
    id: number;
    userId: string;
    totalAmount: number;
    discountAmount: number;
    finalAmount: number;
    orderDate: string;
    shippingAddress: string;
    status: number;
    items: OrderItem[];
    createdAt: string;
    completedAt: string | null;
}

export interface OrderItem {
    id: number;
    orderId: number;
    productId: number;
    productName: string;
    quantity: number;
    price: number;
}