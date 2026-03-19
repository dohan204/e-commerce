
export type OrderResponse = {
    id: number,
    orderCode: string,
    userId: string,
    totalAmount: number,
    discountAmount: number,
    shippingFee: number,
    finalAmount: number,
    status: number,
    paymentMethod: number,
    paymentStatus: number,
    shippingAddress: string,
    voucherId: number,
    note: string,
    createdAt: string,
    completedAt: string,
    items: orderItemResponse[]
    itemCount: number
}

export type orderItemResponse = {
    orderId: number,
    productId: number,
    quantity: number,
    price: number,
    id: number
}


export type Revence = {
    date: string,
    value: number,
}